using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class SyncTest : NetworkBehaviour
{
    // Start is called before the first frame update
    [SyncVar]
    List<GameObject> Players = new List<GameObject>();

    public override void OnStartAuthority() {
        base.OnStartAuthority();
        Debug.Log("OnStartAuthority --> SyncTest");
    }
    void Start()
    {
        Debug.Log("Start()---> SyncTest");

        GameObject PlayerInfo = (GameObject)Instantiate(Resources.Load("PlayerInfo"));
        //NetworkServer.Spawn(PlayerInfo);
        PlayerInfo.GetComponent<PlayerInfoData>().setUser(GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser());
        Players.Add(PlayerInfo);
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("UsersListTextData").GetComponentInChildren<Text>().text = Players.Count.ToString();
        
    }
}
