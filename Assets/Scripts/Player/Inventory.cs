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
    }

    public void SetItem(Interactable item)
    {
        item.GetComponent<Interactable>().Load();
        Destroy(item.gameObject);
    }


}
