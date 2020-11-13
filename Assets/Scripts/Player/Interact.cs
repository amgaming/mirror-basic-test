using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Interact : NetworkBehaviour
{

    private float currentInteractionTimeElapsed = 0f;
    private Interactable itemFound;
    private GameObject interactionUI;
    private Image progressImage;
    private bool isEnabled = true;

    void Start()
    {
        interactionUI = GameObject.Find("InteractionUI");
        progressImage = GameObject.Find("InteractionProgressImage").GetComponent<Image>();
    }

    private void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (HasItem() && isEnabled)
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

    private void IncrementInteractionTime()
    {
        currentInteractionTimeElapsed += Time.deltaTime;
        if (currentInteractionTimeElapsed >= itemFound.GetPickupTime())
        {
            itemFound.Pickup();
            currentInteractionTimeElapsed = 0f;
        }
    }

    private void UpdateProgressImage()
    {
        if (HasItem())
        {
            float percentage = currentInteractionTimeElapsed / itemFound.GetPickupTime();
            progressImage.fillAmount = percentage;
        }
    }

    public void SetItem(Interactable item)
    {
        itemFound = item;
    }

    public void Enable(bool enabled)
    {
        isEnabled = enabled;
    }

}
