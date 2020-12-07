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
public class ItemTrap : NetworkBehaviour
{
    public float pickupTime = 2f;
    public string description = "";
    private int countOnTriggerEnter = 0;

    private GameObject localPlayer;
    private Text descriptionUI;
    private string localPlayerTag = "LocalPlayer";
    public bool isActive = true;
    public bool triggerCondition = false;
    public string effectName;
    public int effectTime = 0;
    public float damage = 0.1f;
    private Collider currentCol;


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
                        new object[] { currentCol, this });
    }

    void Update()
    {
        if (isActive) {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        if (triggerCondition) {
            Trigger();
            triggerCondition = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        currentCol = col;

        if (col.name != localPlayerTag)
        {
            return;
        }

        Debug.Log("call countOnTriggerEnter++  ");


        SetTrap(true);

        if (countOnTriggerEnter > 1) {
            triggerCondition = true;
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
