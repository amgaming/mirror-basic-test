using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Interact : NetworkBehaviour
{

    private float currentInteractionTimeElapsed = 0f;
    private ItemCollider itemFound;
    private GameObject interactionUI;
    private Image progressImage;
    private GameObject playerGameObject;
    public bool itemActive;

    void Start()
    {
        interactionUI = GameObject.Find("InteractionUI");
        progressImage = GameObject.Find("InteractionProgressImage").GetComponent<Image>();
        playerGameObject = GameObject.FindWithTag("Player");
        itemActive = false;
    }

    private void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (HasItem() || itemActive)
        {
            if (!itemActive)
            {
                interactionUI.SetActive(true);
            }

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
            if (itemActive)
            {
                itemActive = false;
                itemFound.ReleaseItem();
                SetItem(null);
            }
            else
            {
                itemActive = true;
                itemFound.Interact();
            }
            currentInteractionTimeElapsed = 0f;
        }
    }

    private void UpdateProgressImage()
    {
        if (HasItem())
        {
            float percentage = currentInteractionTimeElapsed / itemFound.GetInteractionTime();
            progressImage.fillAmount = percentage;
        }
    }

    public void SetItem(ItemCollider item)
    {
        if (itemActive)
        {
            return;
        }
        itemFound = item;
    }

}
