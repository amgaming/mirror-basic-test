using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Inventory : NetworkBehaviour
{
    private Interactable itemToInteractWith;
    
    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        if(!HasItem())
        {
            return;
        }

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.F))
        {
            itemToInteractWith.Enable();
        }

        if (Input.GetMouseButton(1))
        {
            itemToInteractWith.Disable();
        }
    }

    public void SetItem(Interactable item)
    {
        itemToInteractWith = item;
        CmdServerPickItem(item.name);
    }

    public bool HasItem()
    {
        return itemToInteractWith != null;
    }

    public void DropItem()
    {
        if(!HasItem())
        {
            return;
        }

        CmdServerDropItem(itemToInteractWith.name);
        itemToInteractWith = null;
    }

    [Command]
    void CmdServerPickItem(string itemName){
        RpcSendPickItemToClients(itemName);
    }

    [ClientRpc]
    void RpcSendPickItemToClients(string itemName){
        
        GameObject item = GameObject.Find(itemName);

        if(item)
        {
            item.SetActive(false);
        }
    }

    [Command]
    void CmdServerDropItem(string itemName){
        RpcSendDropItemToClients(itemName);
    }

    [ClientRpc]
    void RpcSendDropItemToClients(string itemName){

        GameObject room = GameObject.FindWithTag("Room");
        GameObject item = room.transform.Find(itemName).gameObject;

        if(item)
        {
            item.SetActive(true);
            item.transform.position = transform.position;
            item.transform.parent = transform;
            item.transform.localPosition = new Vector3(1f, 0.5f, 4f);
            item.gameObject.transform.parent = room.transform;
        }
    }


}
