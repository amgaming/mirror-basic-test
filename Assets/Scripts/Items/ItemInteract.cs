using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ItemInteract : NetworkBehaviour
{
    public float interactionTime = 2f;

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


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("OnTriggerEnter SPHERE COLLIDER " + col.name);
        
        if(col.name != "LocalPlayer"){
            return;
        }

        Interact interact = GetLocalPlayer().GetComponent<Interact>();

        if(interact != null){
            interact.SetItem(this);
        }
    }

    void OnTriggerExit(Collider col)
    {

        Debug.Log("OnTriggerExit SPHERE COLLIDER " + col.name);
        
        if(col.name != "LocalPlayer"){
            return;
        }

        Interact interact = GetLocalPlayer().GetComponent<Interact>();

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
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponentInChildren<Item>().setCharged(false);
        GetLocalPlayer().GetComponent<FPSInput>().addItem(this);
        // gameObject.transform.parent = FPSInput.LocalPlayerController.transform;
        // gameObject.transform.localPosition =  new Vector3(0, 0, 0);
    }

    private void release()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
        GetLocalPlayer().GetComponent<FPSInput>().releaseItem();
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
