using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class NetworkCustom : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
     
        Debug.Log("OnClientConnect Called");
 
        base.OnClientConnect(conn);

        //GamePlayerMng.users.Add(GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser());
        GameObject gamePlayerMngGnrlObject = GameObject.Find("GamePlayerMngGnrlObject");
        GamePlayerMngGnrl gamePlayerMngGnrl = gamePlayerMngGnrlObject.GetComponentInChildren<GamePlayerMngGnrl>();
        List<ListUser> users = gamePlayerMngGnrl.users;
        users.Add(GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser());
        GameObject.Find("UsersListTextData").GetComponentInChildren<Text>().text = users.Count.ToString();
    }
}
