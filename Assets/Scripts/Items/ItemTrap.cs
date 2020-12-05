using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/* [RequireComponent(typeof(Rigidbody))] */
/* [RequireComponent(typeof(SphereCollider))] */
/* [RequireComponent(typeof(BoxCollider))] */
public class ItemTrap : NetworkBehaviour
{
    public float pickupTime = 2f;
    public string description = "";
    private int countOnTriggerEnter = 0;

    private GameObject localPlayer;
    private GameObject inventoryUI;
    private Text descriptionUI;
    private string localPlayerTag = "LocalPlayer";


    protected GameObject GetLocalPlayer()
    {

        if (localPlayer != null)
        {
            return localPlayer;
        }

        localPlayer = GameObject.Find(localPlayerTag);

        return localPlayer;
    }

    void Start()
    {
        inventoryUI = GameObject.Find("InventoryUI");
        if (inventoryUI)
        {
            descriptionUI = inventoryUI.GetComponentInChildren<Text>();

        }
    }

    void Update()
    {

        if (GetLocalPlayer() == null)
        {
            return;
        }

        if (GetLocalPlayer().GetComponent<Inventory>().HasItem())
        {
            inventoryUI.SetActive(true);
        }
        else
        {
            inventoryUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {

        Debug.Log("OnTriggerEnter SPHERE COLLIDER 1 " + col.name);

        if (col.name != localPlayerTag)
        {
            return;
        }

        Debug.Log("call countOnTriggerEnter++  ");


        SetTrap(true);

        if (countOnTriggerEnter > 1 && GetComponent<Trap>()) {
            GetComponent<Trap>()._OnTriggerEnter(col);
        }
    }

    private void OnTriggerExit(Collider col)
    {

        Debug.Log("OnTriggerExit SPHERE COLLIDER 1 " + col.name);

        if (col.name != localPlayerTag)
        {
            return;
        }

        SetTrap(false);
    }

    private void SetTrap(bool item)
    {
        if (item == false)
        {
            countOnTriggerEnter--;
            GetLocalPlayer().GetComponent<Interact>().SetTrap(null);
        }
        else
        {
            countOnTriggerEnter++;
            GetLocalPlayer().GetComponent<Interact>().SetTrap(this);

        }
    }

    public void Deactivate(int seconds)
    {
        GetComponent<Trap>().setActive(false);
        if (seconds > 0) {
            StartCoroutine(activateAfterSeconds(seconds));
        }
    }
    private IEnumerator activateAfterSeconds(int seconds) { 
        yield return new WaitForSeconds(seconds);    
        GetComponent<Trap>().setActive(true);  
    }

    public void Drop()
    {
        GetLocalPlayer().GetComponent<Inventory>().DropItem();
    }

    public void Enable()
    {
        Drop();
    }

    public void Disable()
    {
        Drop();
    }

    public float GetPickupTime()
    {
        return pickupTime;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }


}
