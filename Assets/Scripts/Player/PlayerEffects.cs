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
    private GameObject userAttributesUI;
    private Image progressImage;

     public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        userAttributesUI = GameObject.Find("UserAttributesUI");
        progressImage = GameObject.Find("UserAttributesUIProgressImage").GetComponent<Image>();
    }

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

    public static void FullHeath(Interactable InteractableObject)
    {
        PlayerEffects playerEffects = GameObject.Find("LocalPlayer").GetComponent<PlayerEffects>();
        
        playerEffects.progressImage.fillAmount = 1;

    }

    public void Damage(float amount)
    {

        if (progressImage.fillAmount > 0) {
            progressImage.fillAmount = progressImage.fillAmount - amount;
            if (progressImage.fillAmount < 0) {
                progressImage.fillAmount = 0;
            }
        }
    }

    public static void Freeze(Collider col, ItemTrap ItemTrapObject)
    {

        GameObject playerGameObject = GameObject.Find("LocalPlayer");
        PlayerEffects playerEffects = playerGameObject.GetComponent<PlayerEffects>();
        
        IEnumerator deactivateAfterSeconds(int seconds) { 
            yield return new WaitForSeconds(seconds);   
        
        playerGameObject.GetComponent<FPSInput>().enableMovement(true);
        playerGameObject.GetComponent<Interact>().Enable(true);
        }

        if(!ItemTrapObject.isActive || col.name != "LocalPlayer" || playerGameObject == null || playerGameObject.GetComponent<PlayerEffects>().isUnvulnerable){
            return;
        }
        
        playerGameObject.GetComponent<FPSInput>().enableMovement(false);
        playerGameObject.GetComponent<Interact>().Enable(false);
        playerEffects.Damage(ItemTrapObject.damage);

        if (ItemTrapObject.effectTime > 0) {
             playerEffects.StartCoroutine(deactivateAfterSeconds(ItemTrapObject.effectTime));
        }

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
