using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject room;
    private static float movementSpeed = 1.0f;
    private Camera _camera;
    List<Vector3> trapPositions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        movementSpeed = 2.50f;
        transform.position += (
            transform.right * Input.GetAxis("Horizontal")
            + transform.forward * Input.GetAxis("Mouse ScrollWheel") * 5.0f
            + transform.up * Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.RotateAround(
                Vector3.zero,
                new Vector3(-Input.GetAxis("Mouse Y"),
                    0.0f,
                    Input.GetAxis("Mouse X")),
                50.0f * Time.deltaTime
            );
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButtonDown(0))
        {
            SetTrampPoint();
        }

    }
    void SetTrampPoint()
    {
        Vector2 inputCursor = Input.mousePosition;
        Vector3 point = new Vector3(inputCursor.x, inputCursor.y, 0);
        Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            TrapPossible target = hitObject.GetComponent<TrapPossible>();
            if (target != null)
            {
                trapPositions.Add(hit.transform.position);
            }
        }
        UserConf.setTrapPositions(trapPositions);
        /* Debug.Log("trapPositions.ToArray().Length");
        Debug.Log(UserConf.trapPositions.ToArray().Length);
        Debug.Log("trapPositions");
        trapPositions.ForEach(x => { Debug.Log(x); }); */
        if (UserConf.trapPositions.ToArray().Length > 4)
        {
            SceneManager.LoadScene(room.name);
        }
    }
}
