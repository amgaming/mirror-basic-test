using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Interact : NetworkBehaviour
{

    private float currentInteractionTimeElapsed = 0f;
    private Interactable itemFound;
    private ItemTrap trapFound;
    private GameObject interactionUI;
    private Image progressImage;
    private bool isEnabled = true;

     public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        interactionUI = GameObject.Find("InteractionUI");
        progressImage = GameObject.Find("InteractionProgressImage").GetComponent<Image>();
    }

    private void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (HasTrap() && isEnabled)
        {
            
            interactionUI.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                IncrementInteractionTime();
            }
            else
            {
                currentInteractionTimeElapsed = 0f;
            }

            UpdateProgressImage();
        }
        else
        {

            interactionUI.SetActive(false);
            currentInteractionTimeElapsed = 0f;
        }
    }

    private bool HasItem()
    {
        return itemFound != null;
    }

    private bool HasTrap()
    {
        return trapFound != null;
    }

    private void IncrementInteractionTime()
    {
        currentInteractionTimeElapsed += Time.deltaTime;
        if (currentInteractionTimeElapsed >= trapFound.GetPickupTime())
        {
            trapFound.Deactivate(5);
            currentInteractionTimeElapsed = 0f;
        }
    }

    private void UpdateProgressImage()
    {
        if (HasTrap())
        {
            float percentage = currentInteractionTimeElapsed / trapFound.GetPickupTime();
            progressImage.fillAmount = percentage;
        }
    }

    public void SetItem(Interactable item)
    {
        itemFound = item;
    }
    public void SetTrap(ItemTrap trap)
    {
        trapFound = trap;
    }

    public void Enable(bool enabled)
    {
        isEnabled = enabled;
    }

}
