using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Start is called before the first frame update
    public void Open()
    {
        gameObject.SetActive(true);    
    }

    // Update is called once per frame
    public void Close()
    {
        gameObject.SetActive(false);    
    }
    public void OnSubmitName(string name) { 
        Debug.Log(name);
    }
    public void OnSpeedValue(float speed) { 
        Debug.Log(speed);
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);    
    }
}
