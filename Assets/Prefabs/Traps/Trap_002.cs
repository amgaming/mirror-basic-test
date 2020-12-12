using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Trap_002 : NetworkBehaviour
{
    private ItemTrap itemTrap;

    private GameObject localPlayer;
    private string localPlayerTag = "LocalPlayer";
    public float damage = 0.1f;
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;


    protected GameObject GetLocalPlayer()
    {

        if (localPlayer != null)
        {
            return localPlayer;
        }

        localPlayer = GameObject.Find(localPlayerTag);

        return localPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        itemTrap = GetComponent<ItemTrap>();

        itemTrap.setEffectParams(new object[] { damage });
        
    }

    void Update()
    {
        if (itemTrap.isActive) {
            fire();
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
    }

    void fire() {
        Ray ray = new Ray(transform.position, transform.forward);  
        RaycastHit hit;  
        if (Physics.SphereCast(ray, 0.75f, out hit)) {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>()) {   
                if (_fireball == null) {  
                    _fireball = Instantiate(fireballPrefab) as GameObject; 
                    _fireball.GetComponent<Fireball_001>().SetTrap(this);
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 6f); 
                    _fireball.transform.rotation = transform.rotation;
                }
            }
        } 
    }

    public void hit() {
        Debug.Log(">>>>>>>>>>>>>> Player hit >>>>>>>>>>>>>>");
        itemTrap.triggerCondition = true;
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.name != "LocalPlayer")
        {
            return;
        }

        SetTrap(true);
    }

    private void OnTriggerExit(Collider col)
    {

        Debug.Log("OnTriggerExit SPHERE COLLIDER 1 " + col.name);

        if (col.name != "LocalPlayer")
        {
            return;
        }

        SetTrap(false);
    }

    private void SetTrap(bool item)
    {
        if (item == false)
        {
            GetLocalPlayer().GetComponent<Interact>().SetTrap(null);
        }
        else
        {
            GetLocalPlayer().GetComponent<Interact>().SetTrap(itemTrap);

        }
    }
}
