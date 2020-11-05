using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ItemCollider : NetworkBehaviour
{

    // public override void OnStartLocalPlayer()
    // {
    //     base.OnStartLocalPlayer();
    //     player = GameObject.FindWithTag("Player").GetComponent<Interact>();
    // }

    void OnTriggerEnter(Collider col)
    {
        Interact interact = FPSInput.LocalPlayer.GetComponent<Interact>();
        
        if(interact != null){
            interact.SetItem(gameObject.transform.GetChild(0).GetComponent<Item>());
        }
    }

    void OnTriggerExit()
    {
        Interact interact = FPSInput.LocalPlayer.GetComponent<Interact>();

        if(interact != null){
            interact.SetItem(null);
        }
    }
}
