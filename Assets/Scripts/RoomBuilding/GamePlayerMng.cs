using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ListItemInitRoom 
{

    public Vector3 position;   
    public Quaternion rotation;   
    public GameObject trap;   
    public ListItemInitRoom(Vector3 val1, Quaternion val2, GameObject val3) {
        position = val1;
        rotation = val2;
        trap = val3;
    }
}


public class ListUser 
{
    public string userId;   
    public List<ListItemInitRoom>  trapPositions;   
    public GameObject room;
    public string mapTitle;   
    public string userName;   
    public ListUser(string val1, List<ListItemInitRoom> val2,GameObject val3,string val4,string val5) {
        Debug.Log($"val1: {val1}, val2: {val2}, val3: {val3}, val4: {val4}, val5: {val5}, ");
        userId = val1;
        trapPositions = val2;
        room = val3;
        mapTitle = val4;
        userName = val5;
    }
}


public class GamePlayerMng : MonoBehaviour
{
    static public List<ListItemInitRoom> trapPositions = new List<ListItemInitRoom>();     
    static public List<ListUser> users = new List<ListUser>();     
    public ListUser user;
    public GameObject room;
    public string roomName;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }


    static public void setTrapPositions(List<ListItemInitRoom> data)
    {
        trapPositions = data;
    }

    public void setRoom(GameObject data, string data2)
    {
        room = data;
        roomName = data2;
    }

    public void setUser(ListUser data)
    {
        user = data;
    }

    public ListUser getUser()
    {
        return user;
    }

    public int getTrapPoints() {
        int points = 0;
        trapPositions.ForEach(item =>
        {
            points += item.trap.GetComponent<ItemTrap>().price;
        });
        return points;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
