using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Mirror;

public class Item : MonoBehaviour
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

    //  public override void OnStartLocalPlayer()
    // {
    //     base.OnStartLocalPlayer();

    //     GameObject playerGameObject = GameObject.FindWithTag("Player");
    //     player = playerGameObject.GetComponent<FPSInput>();

    // }
    void Start()
    {
        gameObject.transform.parent.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
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
            setCharged(false);

            enablePlayerMovement(true);

            Interact interact = GetLocalPlayer().GetComponent<Interact>();

            if (interact != null)
            {
                interact.SetItem(gameObject.transform.parent.GetComponent<ItemInteract>());
            }

            currentIntervalElapsed = 0f;
        }
    }

    public void setCharged(bool state)
    {
        if (state) {
            gameObject.transform.parent.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.5f, 0.1f, 0.8f));
        }
        else {
            gameObject.transform.parent.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
        isCharged = state;
    }

    void enablePlayerMovement(bool state)
    {

        GameObject playerGameObject = GetLocalPlayer();

        if (playerGameObject != null)
        {
            playerGameObject.GetComponent<FPSInput>().enableMovement(state);
            isTrapped = !state;
        }
    }

    void OnTriggerEnter(Collider col)
    {

        Debug.Log("OnTriggerEnter BOX COLLIDER " + col.name);
        if(col.name != "LocalPlayer"){
            return;
        }

        if(!isCharged) {
            return;
        }
        
        Interact interact = GetLocalPlayer().GetComponent<Interact>();

        if (interact != null)
        {
            interact.SetItem(null);
        }
        enablePlayerMovement(false);

    }
}