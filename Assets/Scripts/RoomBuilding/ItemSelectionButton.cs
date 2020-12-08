using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectionButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButton(string buttonName) {
        Debug.Log("SelectItemActive");
        Debug.Log(buttonName);
    }
}
