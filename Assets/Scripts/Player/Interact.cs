using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Interact : NetworkBehaviour
{
    [SerializeField]
    [Tooltip("The player prefab camera. A reference to optimize performance")]
    private Camera camera;
    [SerializeField]
    [Tooltip("All the object the user can interact with based on the layer the objects are placed")]
    private LayerMask layerMask;
    [SerializeField]
    [Tooltip("The interaction cool down time")]
    private float interactionTime = 2f;
    private float currentInteractionTimeElapsed = 0f;
    private Item itemToInteractWith;
    [SerializeField]
    private RectTransform interactionUiTextPanel;
    [SerializeField]
    private Image progressImage;

    private void Update()
    {

        if(!isLocalPlayer){ 
            return;
        }

        selectItemFromCameraRay();

        if(HasSelectedItem()){

            interactionUiTextPanel.gameObject.SetActive(true);

            if(Input.GetKey(KeyCode.E)){
                IncrementInteractionTime();
            }
            else {
                currentInteractionTimeElapsed = 0f;
            }

            UpdateProgressImage();
        }
        else {

            interactionUiTextPanel.gameObject.SetActive(false);
            currentInteractionTimeElapsed = 0f;
        }
    }

    private bool HasSelectedItem(){
        return itemToInteractWith != null;
    }

    private void IncrementInteractionTime(){
        currentInteractionTimeElapsed += Time.deltaTime;
        if(currentInteractionTimeElapsed >= interactionTime){
            MoveItemToInventory();
        }
    }

    private void UpdateProgressImage(){
        float percentage = currentInteractionTimeElapsed / interactionTime;
        progressImage.fillAmount = percentage;
    }

    private void MoveItemToInventory(){
        //@todo: move this logic to interactable class in each specific item
        Destroy(itemToInteractWith.gameObject);
        itemToInteractWith = null;
    }

    private void selectItemFromCameraRay()
    {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2.0f);
        // Debug.DrawRay(ray.origin, ray.direction * 2.0f, Color.red);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 2.0f, layerMask)){
            var itemFound = hit.collider.GetComponent<Item>();
            if(itemFound != null && itemFound != itemToInteractWith){
                itemToInteractWith = itemFound;
            }
            else{
                itemFound = null;
            }
        } 
        else{
            itemToInteractWith = null;
        }
    }

}
