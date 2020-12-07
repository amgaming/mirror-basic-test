using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Trap_001 : NetworkBehaviour
{
    private ItemTrap itemTrap;
    private int countOnTriggerEnter = 0;

    private GameObject localPlayer;
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
    // Start is called before the first frame update
    void Start()
    {
        itemTrap = GetComponent<ItemTrap>();
        
    }

    void Update()
    {
        if (itemTrap.isActive) {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        itemTrap.setEffectParams(new object[] { col, itemTrap });

        if (col.name != "LocalPlayer")
        {
            return;
        }

        SetTrap(true);

        if (countOnTriggerEnter > 1) {
            itemTrap.triggerCondition = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {

        Debug.Log("OnTriggerExit SPHERE COLLIDER 1 " + col.name);

        if (col.name != "LocalPlayer")
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
            GetLocalPlayer().GetComponent<Interact>().SetTrap(itemTrap);

        }
    }
}
