using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButtonHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Room"  + (AppSceneManager.roomIndex+1));
    }


}
