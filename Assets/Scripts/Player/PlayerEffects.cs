using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerEffects : NetworkBehaviour
{

    public bool isUnvulnerable = false;
    private string localPlayerTag = "LocalPlayer";
    private GameObject localPlayer;
    private GameObject inventoryUI;
    private Text descriptionUI;

    void Start()
    {
        localPlayer = GameObject.Find(localPlayerTag);
        inventoryUI = GameObject.Find("InventoryUI");
        if (inventoryUI)
        {
            descriptionUI = inventoryUI.GetComponentInChildren<Text>();

        }
    }
    
    void Update()
    {
        if (isUnvulnerable) {
            GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        } else {
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
    }

    public static void Invulnerable(Interactable InteractableObject)
    {
        PlayerEffects playerEffects = GameObject.Find("LocalPlayer").GetComponent<PlayerEffects>();

        playerEffects.isUnvulnerable = true;

        playerEffects.descriptionUI.text = "Invulnerable";
        
        IEnumerator deactivateAfterSeconds(int seconds) { 
            yield return new WaitForSeconds(seconds);    
            playerEffects.isUnvulnerable = false;  
            playerEffects.descriptionUI.text = "";
        }

        if (InteractableObject.effectTime > 0) {
             playerEffects.StartCoroutine(deactivateAfterSeconds(InteractableObject.effectTime));
        }
    }
    public static void SumHealth(Interactable InteractableObject)
    {
        Debug.Log("SumHealth effect ......");
    }
    /* public void Load(string methodName, Interactable InteractableObject)
    {
        // Get the Type for the class
        //Type calledType = Type.GetType("PlayerEffects");
        Type calledType = typeof(PlayerEffects);

        // Invoke the method itself. The string returned by the method winds up in s.
        // Note that InteractableObject is passed via the last parameter of InvokeMember,
        // as an array of Objects.
        calledType.InvokeMember(
                        methodName,
                        BindingFlags.InvokeMethod | BindingFlags.Public | 
                            BindingFlags.Static,
                        null,
                        null,
                        new object[] { InteractableObject });
    } */

    /* public static void InvokeStringMethod(string typeName, string methodName, Interactable InteractableObject)
    {
        // Get the Type for the class
        Type calledType = Type.GetType(typeName);

        // Invoke the method itself. The string returned by the method winds up in s.
        // Note that InteractableObject is passed via the last parameter of InvokeMember,
        // as an array of Objects.
        calledType.InvokeMember(
                        methodName,
                        BindingFlags.InvokeMethod | BindingFlags.Public | 
                            BindingFlags.Static,
                        null,
                        null,
                        new object[] { InteractableObject });
    } */


}
