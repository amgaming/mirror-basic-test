using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ItemCollider : NetworkBehaviour
{
    public float interactionTime = 2f;

    // public override void OnStartLocalPlayer()
    // {
    //     base.OnStartLocalPlayer();
    //     player = GameObject.FindWithTag("Player").GetComponent<Interact>();
    // }

    void OnTriggerEnter(Collider col)
    {
        Interact interact = FPSInput.LocalPlayer.GetComponent<Interact>();
        
        if(interact != null){
            interact.SetItem(gameObject.transform.GetComponent<ItemCollider>());
        }
    }

    void OnTriggerExit()
    {
        Interact interact = FPSInput.LocalPlayer.GetComponent<Interact>();

        if(interact != null){
            interact.SetItem(null);
        }
    }

    public void Interact()
    {
        moveItem();
    }

    public void moveItem()
    { 
        FPSInput.LocalPlayer.GetComponent<FPSInput>().addItem(this.GetComponent<ItemCollider>());
        // gameObject.transform.parent = FPSInput.LocalPlayerController.transform;
        // gameObject.transform.localPosition =  new Vector3(0, 0, 0);
    }

    public float GetInteractionTime(){
        return interactionTime;
    }
}
