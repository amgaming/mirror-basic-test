using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;

public class NetworkCustom : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
     
        Debug.Log("OnClientConnect Called");
 
        base.OnClientConnect(conn);
    }
}
