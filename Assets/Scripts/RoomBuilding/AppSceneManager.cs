using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppSceneManager : MonoBehaviour
{
    public GameObject[] rooms;
    public Camera _camera;
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
        _camera.GetComponent<CameraController>().room = room;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
