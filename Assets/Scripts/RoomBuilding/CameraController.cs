using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject room;
    private static float movementSpeed = 1.0f;
    private Camera _camera;
    public int initPoints = 5;
    private Text textPointsValue;
    private GameObject currentItemObject;
    private GameObject roomBuildingUI;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        roomBuildingUI = GameObject.Find("RoomBuildingUI");
        if (roomBuildingUI)
        {
            textPointsValue = roomBuildingUI.GetComponentInChildren<Text>();
            textPointsValue.text=initPoints.ToString();
        }

    }

    public void OnClick(GameObject itemObject) {
        currentItemObject = itemObject;
        ItemTrap currentItemObjectTrap = itemObject.GetComponent<ItemTrap>();
        GameObject.Find("TitleCurrentTrap").GetComponent<Text>().text=currentItemObjectTrap.title ;
        GameObject.Find("MetaCurrentTrap").GetComponent<Text>().text="Points Cost: " + currentItemObjectTrap.price.ToString();
        GameObject.Find("DescCurrentTrap").GetComponent<Text>().text=currentItemObjectTrap.description;
        /* GameObject slot = GameObject.Find("Slot3");
        slot.GetComponentInChildren<Text>().text=itemObject.GetComponent<ItemTrap>().title; */
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
                ListItemInitRoom newItem = new ListItemInitRoom(hit.transform.position, currentItemObject);
                UserConf.trapPositions.Add(newItem);
            }
        }
        textPointsValue.text=(initPoints - GetComponent<UserConf>().getTrapPoints()).ToString();
        if (GetComponent<UserConf>().getTrapPoints() == initPoints)
        {
            SceneManager.LoadScene(room.name);
        }
    }
}
