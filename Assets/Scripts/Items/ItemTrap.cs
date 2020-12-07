﻿using System;
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
public class ItemTrap : NetworkBehaviour
{
    public float pickupTime = 2f;
    public string description = "";

    private GameObject localPlayer;
    private Text descriptionUI;
    private string localPlayerTag = "LocalPlayer";
    public bool isActive = true;
    public bool triggerCondition = false;
    public string effectName;
    public int effectTime = 0;
    public float damage = 0.1f;
    private Collider currentCol;
    private object[] effectParams;

    void Start()
    {
    }

    public void setEffectParams(object[] val)
    {
        effectParams = val;
    }

    public void setActive(bool val)
    {
        isActive = val;
    }

    public void Trigger()
    {
        Type calledType = typeof(PlayerEffects);
        calledType.InvokeMember(
                        effectName,
                        BindingFlags.InvokeMethod | BindingFlags.Public | 
                            BindingFlags.Static,
                        null,
                        null,
                        effectParams);
    }

    void Update()
    {
        if (triggerCondition) {
            Trigger();
            triggerCondition = false;
        }
    }

    public void Deactivate(int seconds)
    {
        setActive(false);
        if (seconds > 0) {
            StartCoroutine(activateAfterSeconds(seconds));
        }
    }
    private IEnumerator activateAfterSeconds(int seconds) { 
        yield return new WaitForSeconds(seconds);    
        setActive(true);  
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
