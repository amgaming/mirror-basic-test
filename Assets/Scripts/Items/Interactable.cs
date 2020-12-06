using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/* [RequireComponent(typeof(Rigidbody))] */
/* [RequireComponent(typeof(SphereCollider))] */
/* [RequireComponent(typeof(BoxCollider))] */
public class Interactable : NetworkBehaviour
{
    public float speedRotate = 200f;
    public string description = "";

    private GameObject localPlayer;
    private string localPlayerTag = "LocalPlayer";
    public string effectName;
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

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate (Vector3.up * speedRotate * Time.deltaTime, Space.World);
    }

    public void Load()
    {
        Type calledType = typeof(PlayerEffects);
        calledType.InvokeMember(
                        effectName,
                        BindingFlags.InvokeMethod | BindingFlags.Public | 
                            BindingFlags.Static,
                        null,
                        null,
                        new object[] { this });
    }

    private void OnTriggerEnter(Collider col)
    {

        Debug.Log("OnTriggerEnter SPHERE COLLIDER 1 " + col.name);

        if (col.name != localPlayerTag)
        {
            return;
        }

        UseItem();
    }
    public void UseItem()
    {
        GetLocalPlayer().GetComponent<Inventory>().SetItem(this);
    }


}
