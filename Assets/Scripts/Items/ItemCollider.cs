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
            interact.SetItem(this);
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

    public void ReleaseItem()
    {
        release();
    }

    public void moveItem()
    { 
        FPSInput.LocalPlayer.GetComponent<FPSInput>().addItem(this);
        // gameObject.transform.parent = FPSInput.LocalPlayerController.transform;
        // gameObject.transform.localPosition =  new Vector3(0, 0, 0);
    }

    private void release()
    {
        FPSInput.LocalPlayer.GetComponent<FPSInput>().releaseItem();
    }

    public float GetInteractionTime(){
        return interactionTime;
    }
}
