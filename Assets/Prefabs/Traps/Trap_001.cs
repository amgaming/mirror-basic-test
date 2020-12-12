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
    public float damage = 0.1f;
    public int effectTime = 0;


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

        itemTrap.setEffectParams(new object[] { itemTrap, damage, effectTime });
        
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
