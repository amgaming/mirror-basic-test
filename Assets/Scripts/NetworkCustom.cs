using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class NetworkCustom : NetworkManager
{
    List<GameObject> Players = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("UsersListTextData").GetComponentInChildren<Text>().text = Players.Count.ToString();
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
     
        Debug.Log("OnServerConnect Called");
 
        base.OnServerConnect(conn);

        //GamePlayerMng.users.Add(GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser());
        GameObject PlayerInfo = (GameObject)Instantiate(Resources.Load("PlayerInfo"));
        PlayerInfo.GetComponent<PlayerInfoData>().setUser(GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser());
        Players.Add(PlayerInfo);
        /* GamePlayerMngGnrl gamePlayerMngGnrl = gamePlayerMngGnrlObject.GetComponentInChildren<GamePlayerMngGnrl>();
        List<ListUser> users = gamePlayerMngGnrl.users;
        users.Add(GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser()); */
        //GameObject.Find("UsersListTextData").GetComponentInChildren<Text>().text = users.Count.ToString();
        //Instantiate(Resources.Load("PlayerInfo"))
    }
}
