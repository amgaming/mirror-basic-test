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

        if (Input.GetMouseButton(0))
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
        itemToInteractWith.SetActive(false);
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

        itemToInteractWith.SetActive(true);
        itemToInteractWith.transform.position =  transform.position;
        itemToInteractWith.transform.parent = transform;
        itemToInteractWith.transform.localPosition =  new Vector3(1f, 0.5f, 4f);
        itemToInteractWith.gameObject.transform.parent = GameObject.FindWithTag("Room").transform;
        itemToInteractWith = null;
    }


}
