using UnityEngine;
 
public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkLobbyManager networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;


    public void onExit()
    {
        Debug.Log("Quitting ...");
        Application.Quit();
    }

    public void EnterMain()
    {
        landingPagePanel.SetActive(true);
    }

    public void HostLobby()
    {
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
    }
}