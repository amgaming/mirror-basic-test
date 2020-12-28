using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mirror;


public class GamePlayerMngGnrl : MonoBehaviour
{   
     public List<ListUser> users = new List<ListUser>(); 
    // Start is called before the first frame update
    void Start()
    {
        /* if (!Convert.ToBoolean(users)) {
            users = new List<ListUser>();  
        } */

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
