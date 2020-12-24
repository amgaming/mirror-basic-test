using UnityEngine;
 
public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkLobbyManager networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;
    
    [Header("UI")]
    [SerializeField] private GameObject lobbyPagePanel = null;

    public void HostLobby()
    {
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
        lobbyPagePanel.SetActive(true);
    }
}