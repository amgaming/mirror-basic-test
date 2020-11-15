using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMiniMapCamera : MonoBehaviour
{
    public GameObject map;
    public float speed = 50.0f;

    // Update is called once per frame
    void Update()
    {
       
        Vector3 translationMovement = new Vector3(0, Input.GetAxis("Horizontal"),  0);
        map.transform.Rotate(translationMovement * speed * Time.deltaTime);
        
    }
}
