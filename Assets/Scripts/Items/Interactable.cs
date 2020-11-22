using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class Interactable : NetworkBehaviour
{
    public float pickupTime = 2f;
    public string description = "";

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
        Debug.Log("OnTriggerEnter SPHERE COLLIDER " + col.name);

        if (col.name != localPlayerTag)
        {
            return;
        }

        SetItem(true);
    }

    private void OnTriggerExit(Collider col)
    {

        Debug.Log("OnTriggerExit SPHERE COLLIDER " + col.name);

        if (col.name != localPlayerTag)
        {
            return;
        }

        SetItem(false);
    }

    private void SetItem(bool item)
    {
        if (item == false)
        {
            GetLocalPlayer().GetComponent<Interact>().SetItem(null);
        }
        else
        {
            GetLocalPlayer().GetComponent<Interact>().SetItem(this);

        }
    }

    public void Pickup()
    {
        // Update local player interface
        descriptionUI.text = description;
        // Drop current inventory active item
        Drop();
        // Set new active inventory item 
        GetLocalPlayer().GetComponent<Inventory>().SetItem(this);
        // Not interacting with item anymore (it's on inventory now)
        SetItem(false);
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
