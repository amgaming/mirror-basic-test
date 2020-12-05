using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Mirror;

public class Trap : MonoBehaviour
{

    private bool isTrapped = false;
    private bool isCharged = false;
    // private FPSInput player;
    public float trappedInterval = 2f;
    private float currentIntervalElapsed = 0f;
    private GameObject localPlayer;

    GameObject GetLocalPlayer()
    {

        if(localPlayer != null)
        {
            return localPlayer;
        }

        localPlayer = GameObject.Find("LocalPlayer");

        return localPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrapped == true)
        {
            checkIsTrapped();
        }
    }

    void checkIsTrapped()
    {

        currentIntervalElapsed += Time.deltaTime;

        if (currentIntervalElapsed >= trappedInterval)
        {
            enablePlayerMovement(true);
            currentIntervalElapsed = 0f;
        }
    }


    void enablePlayerMovement(bool state)
    {

        GameObject playerGameObject = GetLocalPlayer();

        if (playerGameObject != null)
        {
            playerGameObject.GetComponent<FPSInput>().enableMovement(state);
            playerGameObject.GetComponent<Interact>().Enable(state);
            isTrapped = !state;
        }
    }

    public void _OnTriggerEnter(Collider col)
    {

        Debug.Log("OnTriggerEnter BOX COLLIDER 2 " + col.name);

        if(col.name != "LocalPlayer"){
            return;
        }

        
        // Stop Player Movement
        enablePlayerMovement(false);
        // Run animation 

    }
}