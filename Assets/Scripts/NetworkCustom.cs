using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;

public class NetworkCustom : NetworkManager
{
    public override void OnServerSceneChanged(string sceneName)
    {
     
        Debug.Log("OnServerSceneChanged called");
        Debug.Log("aqui ---------------------");
        Debug.Log(SceneManager.GetActiveScene().name);
        Debug.Log("aqui ---------------------");
        foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
        {
            //GameObject obj = conn.identity.gameObject;

            NetworkServer.ReplacePlayerForConnection(conn, (GameObject)Instantiate(Resources.Load("Player")));

            //NetworkServer.Destroy(obj);
        }
    }
}
