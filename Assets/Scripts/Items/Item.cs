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


    //  public override void OnStartLocalPlayer()
    // {
    //     base.OnStartLocalPlayer();

    //     GameObject playerGameObject = GameObject.FindWithTag("Player");
    //     player = playerGameObject.GetComponent<FPSInput>();

    // }
    void Start()
    {
        this.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.white);
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

            Interact interact = FPSInput.LocalPlayer.GetComponent<Interact>();

            if (interact != null)
            {
                interact.SetItem(this.gameObject.transform.parent.GetComponent<ItemCollider>());
            }

            currentIntervalElapsed = 0f;
        }
    }

    public void setCharged(bool state)
    {
        if (state) {
            this.GetComponentInChildren<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.5f, 0.1f, 0.8f));
        }
        else {
            this.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.white);
        }
        isCharged = state;
    }

    void enablePlayerMovement(bool state)
    {

        GameObject playerGameObject = GameObject.FindWithTag("Player");

        if (playerGameObject != null)
        {
            playerGameObject.GetComponent<FPSInput>().enableMovement(state);
            isTrapped = !state;
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if(col.tag != "Player"){
            return;
        }

        if(!isCharged) {
            return;
        }
        
        Interact interact = FPSInput.LocalPlayer.GetComponent<Interact>();

        if (interact != null)
        {
            interact.SetItem(null);
        }
        enablePlayerMovement(false);
    }
}