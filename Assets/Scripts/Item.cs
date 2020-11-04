using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Item : NetworkBehaviour
{

    private bool isTrapped = false;
    // private FPSInput player;
    public float trappedInterval = 2f;
    private float currentIntervalElapsed = 0f;

    //  public override void OnStartLocalPlayer()
    // {
    //     base.OnStartLocalPlayer();

    //     GameObject playerGameObject = GameObject.FindWithTag("Player");
    //     player = playerGameObject.GetComponent<FPSInput>();

    // }


    // Update is called once per frame
    void Update()
    {
        if(isTrapped == true)
        {
            checkIsTrapped();
        }
    }

    void checkIsTrapped(){

        currentIntervalElapsed += Time.deltaTime;

        if(currentIntervalElapsed >= trappedInterval){
            enablePlayerMovement(true);
            currentIntervalElapsed = 0f;
        }
    }

    void enablePlayerMovement(bool state){

        GameObject playerGameObject = GameObject.FindWithTag("Player");
       
        if(playerGameObject != null){
            Debug.Log("aqui " + state);
            playerGameObject.GetComponent<FPSInput>().enableMovement(state);
            isTrapped = !state;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("trampa pisada! ", col);
        //enablePlayerMovement(false);
    }
}