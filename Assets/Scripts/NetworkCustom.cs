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
        List<ListUser> users = GameObject.Find("GamePlayerMngGnrlObject").GetComponentInChildren<GamePlayerMngGnrl>().users;
        users.Add(GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser());
        GameObject.Find("UsersListTextData").GetComponentInChildren<Text>().text = users.Count.ToString();
    }
}
