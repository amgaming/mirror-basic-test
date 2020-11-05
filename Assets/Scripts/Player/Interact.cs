using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Interact : NetworkBehaviour
{
 
    private float currentInteractionTimeElapsed = 0f;
    private Item itemFound;
    private GameObject interactionUI;
    private Image progressImage;

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

        if (HasItem() )
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
        if (currentInteractionTimeElapsed >= itemFound.GetInteractionTime())
        {
            itemFound.Interact();
        }
    }

    private void UpdateProgressImage()
    {
        float percentage = currentInteractionTimeElapsed / itemFound.GetInteractionTime();
        progressImage.fillAmount = percentage;
    }

    public void SetItem(Item item)
    {
        itemFound = item;
    }

}
