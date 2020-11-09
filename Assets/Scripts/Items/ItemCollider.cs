using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ItemCollider : NetworkBehaviour
{
    public float interactionTime = 2f;


    void OnTriggerEnter(Collider col)
    {

        if(col.tag != "Player"){
            return;
        }

        Interact interact = FPSInput.LocalPlayer.GetComponent<Interact>();

        if(interact != null){
            interact.SetItem(this);
        }
    }

    void OnTriggerExit(Collider col)
    {

        if(col.tag != "Player"){
            return;
        }

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
        this.GetComponentInChildren<Rigidbody>().useGravity = false;
        this.GetComponentInChildren<Item>().setCharged(false);
        FPSInput.LocalPlayer.GetComponent<FPSInput>().addItem(this);
        // gameObject.transform.parent = FPSInput.LocalPlayerController.transform;
        // gameObject.transform.localPosition =  new Vector3(0, 0, 0);
    }

    private void release()
    {
        this.GetComponentInChildren<Rigidbody>().useGravity = true;
        FPSInput.LocalPlayer.GetComponent<FPSInput>().releaseItem();
        StartCoroutine(setCharged());
    }

    private IEnumerator setCharged()
    {
        yield return new WaitForSeconds(1);

        this.GetComponentInChildren<Item>().setCharged(true);
    }

    public float GetInteractionTime(){
        return interactionTime;
    }
}
