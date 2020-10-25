using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController))]
public class FPSInput : NetworkBehaviour
{
    public float speed = 6.0f;
    private CharacterController _charController;
    public const float baseSpeed = 6.0f;
    // Start is called before the first frame update

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Turn off main camera because GamePlayer prefab has its own camera
        GetComponentInChildren<Camera>().enabled = true;

        if(Camera.main){
            Camera.main.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        { 
        
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            //movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);
        }
    }
    void Awake()
    {   
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    private void OnSpeedChanged(float value)
    {
        //speed = baseSpeed * value;
    }

    /* public override void OnStartLocalPlayer()
    {
        _charController.enabled = true;

        Camera.main.orthographic = false;
        //Camera.main.enabled = true;
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0f, 3f, -8f);
        Camera.main.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
    } */
}
