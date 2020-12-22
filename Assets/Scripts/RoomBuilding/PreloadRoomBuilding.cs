using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class PreloadRoomBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("RoomBuilding");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
