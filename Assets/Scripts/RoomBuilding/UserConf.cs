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


public class UserConf : MonoBehaviour
{
    static public List<ListItemInitRoom> trapPositions = new List<ListItemInitRoom>();    
    public string userId = getUserId(5);   
    public GameObject room;
    public string roomName;
    public static string getUserId(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars);
    }
    // Start is called before the first frame update
    void Start()
    {

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
