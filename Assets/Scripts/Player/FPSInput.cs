﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(AudioNetwork))]
[RequireComponent(typeof(CharacterController))]
public class FPSInput : NetworkBehaviour
{
    public float speed = 6.0f;
    public float sprintSpeedFactor = 2.0f;
    private CharacterController _charController;
    bool isRunningSoundPlaying;
    private Vector3 translationMovement;
    private AudioNetwork audioNetwork;
    private bool isPlayerSprinting;
    private bool isPlayerMovementEnabled = true;

    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        audioNetwork = GetComponent<AudioNetwork>();
        transform.parent = GameObject.FindWithTag("Room").transform;
    }


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Turn off main camera because GamePlayer prefab has its own camera
        camera = GetComponentInChildren<Camera>();
        
        camera.enabled = true;

        if (Camera.main)
        {
            Camera.main.enabled = false;
        }

        gameObject.name = "LocalPlayer";
    }

    // public static GameObject GetLocalPlayer()
    // {
    // }
 

    // Update is called once per frame
    void Update()
    {

        if (isLocalPlayer)
        {

            if (!IsPlayerMovementEnabled())
            {
                return;
            }

            float finalSpeed = speed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                finalSpeed = speed * sprintSpeedFactor;
            }

            float deltaX = Input.GetAxis("Horizontal") * finalSpeed;
            float deltaZ = Input.GetAxis("Vertical") * finalSpeed;
            
            // translationMovement = new Vector3(deltaX, 0, deltaZ);
            translationMovement = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * new Vector3(deltaX, -9.8f, deltaZ);
            translationMovement = Vector3.ClampMagnitude(translationMovement, finalSpeed);

            translationMovement *= Time.deltaTime;
            // translationMovement = transform.TransformDirection(translationMovement);
            _charController.Move(translationMovement);


            if (Input.GetKey(KeyCode.LeftShift) && (deltaX != 0 || deltaZ != 0))
            {
                if (isPlayerSprinting != true)
                {
                    isPlayerSprinting = true;
                    audioNetwork.PlaySound(0);
                }
            }
            else
            {
                isPlayerSprinting = false;
                audioNetwork.StopSound();
            }
        }
    }


    public void enableMovement(bool state)
    {
        isPlayerMovementEnabled = state;
    }

    public bool IsPlayerMovementEnabled()
    {
        return isPlayerMovementEnabled;
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
