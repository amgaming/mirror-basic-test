using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Mirror;

public class Trap : MonoBehaviour
{

    private bool isTrapped = false;
    private bool isCharged = false;
    private bool isActive = true;
    // private FPSInput player;
    public float trappedInterval = 2f;
    public float damage = 0.1f;
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
        if (isActive) {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
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

        if (playerGameObject != null && !playerGameObject.GetComponent<PlayerEffects>().isUnvulnerable)
        {
            playerGameObject.GetComponent<FPSInput>().enableMovement(state);
            playerGameObject.GetComponent<Interact>().Enable(state);
            isTrapped = !state;
        }
    }

    public void setActive(bool val)
    {

        isActive = val;

    }

    public void _OnTriggerEnter(Collider col)
    {

        GameObject playerGameObject = GetLocalPlayer();

        Debug.Log("OnTriggerEnter BOX COLLIDER 2 " + col.name);

        if(!isActive || col.name != "LocalPlayer"){
            return;
        }

        
        // Stop Player Movement
        enablePlayerMovement(false);
        
        PlayerEffects.Damage(damage);
        // Run animation 

    }
}