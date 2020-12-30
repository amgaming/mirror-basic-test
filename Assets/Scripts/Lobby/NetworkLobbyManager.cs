using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkLobbyManager : NetworkManager
{
    [SerializeField] private int minPlayers = 2;
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkLobbyPlayer lobbyPlayerPrefab = null;
    [SerializeField] private GameObject UICanvas = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection> OnServerIsReady;
    public static event Action OnServerStopped;

    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public List<NetworkLobbyPlayer> RoomPlayers { get; } = new List<NetworkLobbyPlayer>();

    private bool IsOnMenuScene()
    {
        return SceneManager.GetActiveScene().path == menuScene;
    }

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (!IsOnMenuScene())
        {
            conn.Disconnect();
            return;
        }
    }
   
    public override void OnServerAddPlayer(NetworkConnection conn)
    {        

        if (IsOnMenuScene())
        {
            bool isLeader = RoomPlayers.Count == 0;

            NetworkLobbyPlayer lobbyPlayerInstance = Instantiate(lobbyPlayerPrefab);

            lobbyPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, lobbyPlayerInstance.gameObject);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkLobbyPlayer>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        OnServerStopped?.Invoke();

        RoomPlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers) { return false; }

        foreach (var player in RoomPlayers)
        {
            if (!player.IsReady) { return false; }
        }

        return true;
    }

    public void StartGame()
    {
        if (IsOnMenuScene())
        {
            if (!IsReadyToStart()) { return; }

            ServerChangeScene("RoomBuilding");
        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        // Here we can overwrite default ServerChangeScene to add some logic
        // when we go from lobby menu to another scene (i.e. RoomBuilding)
        // for example if we want to init some prefabs or some player config

        if (IsOnMenuScene() && newSceneName.StartsWith("RoomBuilding"))
        {
            // for (int i = RoomPlayers.Count - 1; i >= 0; i--)
            // {
            //     var conn = RoomPlayers[i].connectionToClient;
                // var gameplayerInstance = Instantiate(gamePlayerPrefab);
                // gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

                // NetworkServer.Destroy(conn.identity.gameObject);

                // NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);
            // }
        }

        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        // This happens once the new scene has changed ok, so we can spawn 
        // some items or whatever we want to do
        
        // if (sceneName.StartsWith("RoomBuilding"))
        // {
        //     GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
        //     NetworkServer.Spawn(playerSpawnSystemInstance);

        //     GameObject roundSystemInstance = Instantiate(roundSystem);
        //     NetworkServer.Spawn(roundSystemInstance);
        // }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        OnServerIsReady?.Invoke(conn);
    }

}
