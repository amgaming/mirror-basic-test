﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GamePlayerMngGnrl : MonoBehaviour
{   
     public List<ListUser> users = new List<ListUser>(); 
    // Start is called before the first frame update
    void Start()
    {
        /* if (!Convert.ToBoolean(users)) {
            users = new List<ListUser>();  
        } */

        Debug.Log("-------------------------????????----------------------------");

        GameObject PlayerInfo = (GameObject)Instantiate(Resources.Load("PlayerInfo"));
        PlayerInfo.GetComponent<PlayerInfoData>().setUser(GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().getUser());

    }

    /* void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    } */
    // Update is called once per frame
    void Update()
    {

    }
}
