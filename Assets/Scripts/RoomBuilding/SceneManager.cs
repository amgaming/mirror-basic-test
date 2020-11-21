using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject[] rooms;
    // Start is called before the first frame update
    void Start()
    {
        /* GameObject room = GameObject.FindWithTag("Room1");
        Instantiate(room);
        room.transform.position = Vector3.zero; */
        int roomId = Random.Range(0, rooms.Length);
        GameObject room = rooms[roomId];
        Instantiate(room);
        room.transform.position = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
