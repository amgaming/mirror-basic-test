using System.Collections;
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
    public bool isPlayerTrapped;

    // Start is called before the first frame update

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        audioNetwork = GetComponent<AudioNetwork>();
        isPlayerTrapped = false;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Turn off main camera because GamePlayer prefab has its own camera
        GetComponentInChildren<Camera>().enabled = true;

        if (Camera.main)
        {
            Camera.main.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isPlayerTrapped);
        if (isLocalPlayer && isPlayerTrapped == false)
        {

            float finalSpeed = speed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                finalSpeed = speed * sprintSpeedFactor;
            }

            float deltaX = Input.GetAxis("Horizontal") * finalSpeed;
            float deltaZ = Input.GetAxis("Vertical") * finalSpeed;
            translationMovement = new Vector3(deltaX, 0, deltaZ);
            translationMovement = Vector3.ClampMagnitude(translationMovement, finalSpeed);
            //movement.y = gravity;
            translationMovement *= Time.deltaTime;
            translationMovement = transform.TransformDirection(translationMovement);
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

    public void toggleTrapped()
    {
        isPlayerTrapped = true;
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
