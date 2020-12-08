using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppSceneManager : MonoBehaviour
{
    public GameObject[] rooms;
    public static GameObject room;
    public static int roomIndex;
    public Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        /* GameObject room = GameObject.FindWithTag("Room1");
        Instantiate(room);
        room.transform.position = Vector3.zero; */
        roomIndex = Random.Range(0, rooms.Length);
        room = Instantiate(rooms[roomIndex]);
        room.transform.position = Vector3.zero;

    }


}
