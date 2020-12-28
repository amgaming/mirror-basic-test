using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class LoginManagment : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ListUser user =  GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser();
        string username =  user.mapTitle;
        string mapTitle =  user.userName;
        string userId =  user.userId;

        GameObject.Find("UserName").GetComponentInChildren<Text>().text = username;
        GameObject.Find("MapName").GetComponentInChildren<Text>().text = mapTitle;
        GameObject.Find("UserId").GetComponentInChildren<Text>().text = userId;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
