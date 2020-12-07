using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_001 : MonoBehaviour
{
    private ItemTrap itemTrap;
    // Start is called before the first frame update
    void Start()
    {
        itemTrap = GetComponent<ItemTrap>();
        
    }

    void Update()
    {
        if (itemTrap.isActive) {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
    }
}
