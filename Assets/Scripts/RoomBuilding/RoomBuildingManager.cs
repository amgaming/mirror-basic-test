using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RoomBuildingManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject room;
    private static float movementSpeed = 1.0f;
    public Camera _camera;
    public int initPoints = 5;
    private Text textPointsValue;
    private GameObject currentItemObject;
    private GameObject roomBuildingUI;
    private GameObject readyButton;
    void Start()
    {
        initUI();
        pickRoom();
    }

    // Update is called once per frame
    void Update()
    {
        CameraManage();
        PutTrap();
    }

    private void initUI() {

        readyButton = GameObject.Find("ReadyButton");
        readyButton.SetActive(false);
        roomBuildingUI = GameObject.Find("RoomBuildingUI");
        if (roomBuildingUI)
        {
            textPointsValue = roomBuildingUI.GetComponentInChildren<Text>();
            textPointsValue.text=initPoints.ToString();
        }
    }

    private void pickRoom() {
        int roomId = Random.Range(0, rooms.Length);
        room = rooms[roomId];
        Instantiate(room);
        room.transform.position = Vector3.zero;
    }

    public void OnTrapSelection(GameObject itemObject) {
        currentItemObject = itemObject;
        SetUiPanelDesc();
    }

    private void SetUiPanelDesc(){
        ItemTrap currentItemObjectTrap = currentItemObject.GetComponent<ItemTrap>();
        string title = currentItemObjectTrap.title;
        string desc = currentItemObjectTrap.description;
        string meta = "Points Cost: " + currentItemObjectTrap.price.ToString();

        GameObject.Find("TitleCurrentTrap").GetComponent<Text>().text = title ;
        GameObject.Find("DescCurrentTrap").GetComponent<Text>().text = desc;
        GameObject.Find("MetaCurrentTrap").GetComponent<Text>().text = meta;
    }

    private void PutTrap() {
        if (Input.GetMouseButtonDown(0))
        {
            SetTrampPoint();
            CheckState();
        }
    }

    private void CameraManage() {
        Cursor.visible = true;
        movementSpeed = 2.50f;
        _camera.transform.position += (
            _camera.transform.right * Input.GetAxis("Horizontal")
            + _camera.transform.forward * Input.GetAxis("Mouse ScrollWheel") * 5.0f
            + _camera.transform.up * Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _camera.transform.RotateAround(
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
                instatiateTraps(newItem);
            }
        }
    }
    private void instatiateTraps(ListItemInitRoom item) {
        Instantiate(item.trap);
        Vector3 position = item.position;
        position.y += 10;
        item.trap.transform.position = position;
    }

    public void Ready() {
        SceneManager.LoadScene(room.name);
    }
    void CheckState(){
        textPointsValue.text=(initPoints - GetComponent<UserConf>().getTrapPoints()).ToString();
        if (GetComponent<UserConf>().getTrapPoints() == initPoints)
        {
            readyButton.SetActive(true);
        }
    }
}
