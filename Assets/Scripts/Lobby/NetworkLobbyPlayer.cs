using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkLobbyPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject lobbyMenu = null;
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private Button readyGameButton = null;
    [SerializeField] private Button notReadyGameButton = null;
    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[3];

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Waiting For Player...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    private bool isLeader;
    public bool IsLeader
    {
        set
        {
            isLeader = value;
        }
    }

    private NetworkLobbyManager room;
    private NetworkLobbyManager Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkLobbyManager;
        }
    }

    private MainMenu uiMenu;
    private MainMenu UIMenu
    {
        get
        {
            if (uiMenu != null) { return uiMenu; }
            return uiMenu = GameObject.FindObjectOfType<MainMenu>();
        }
    }

    public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();
    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();
    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader) { return; }

        startGameButton.gameObject.SetActive(readyToStart);
    }


    public void OnExitClick()
    {
        if(isLeader){
            Room.StopHost();
        }
        else
        {
            Room.StopClient();
        }

        lobbyMenu.SetActive(false);

        UIMenu.EnterMain();

    }

    public void OnReadyClick()
    {
        readyGameButton.gameObject.SetActive(false);
        nameInputField.interactable = false;
        notReadyGameButton.gameObject.SetActive(true);
        CmdReadyUp(PlayerNameInput.DisplayName);
    }

    public void OnCancelReadyClick()
    {
        notReadyGameButton.gameObject.SetActive(false);
        readyGameButton.gameObject.SetActive(true);
        nameInputField.interactable = true;
        CmdReadyDown();
    }

    public override void OnStartAuthority()
    {
        lobbyMenu.SetActive(true);
    }

    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);

        UpdateDisplay();
    }

    public override void OnNetworkDestroy()
    {
        Room.RoomPlayers.Remove(this);

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (!hasAuthority)
        {
            foreach (var player in Room.RoomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }

        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting For Player...";
        }

        var index = 0;

        for (int i = 0; i < Room.RoomPlayers.Count; i++)
        {
            if(!Room.RoomPlayers[i].hasAuthority && Room.RoomPlayers[i].IsReady)
            {
                playerNameTexts[index].text = Room.RoomPlayers[i].DisplayName + " is ready";
                index++;
            }
        }
    }

    [Command]
    public void CmdReadyUp(string displayName)
    {
        IsReady = true;
        DisplayName = displayName;
        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdReadyDown()
    {
        IsReady = false;
        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        if (Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }

        Room.StartGame();
    }

}
