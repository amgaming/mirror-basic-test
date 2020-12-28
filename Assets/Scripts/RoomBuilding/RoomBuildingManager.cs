using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RoomBuildingManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject room;
    private static float movementSpeed = 1.0f;
    public Camera _camera;
    public GameObject _cameraBody;
    private CharacterController _charController;
    public int initPoints = 5;
    private Text textPointsValue;
    private GameObject currentItemObject;
    private GameObject roomBuildingUI;
    private GameObject readyButton;
    private string userId;   
    void Start()
    {
        userId = getUserId(5);  
        initUI();
        pickRoom();
    }
    public static string getUserId(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[length];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(0, chars.Length)];
        }

        return new String(stringChars);
    }

    // Update is called once per frame
    void Update()
    {
        CameraManage();
        PutTrap();
    }

    private void initUI() {
        GameObject.Find("UserIdUiBlock").GetComponentInChildren<Text>().text=userId;
        _cameraBody = GameObject.Find("CameraBody");
        _charController = _cameraBody.GetComponent<CharacterController>();
        readyButton = GameObject.Find("ReadyButton");
        //readyButton.SetActive(false);
        roomBuildingUI = GameObject.Find("RoomBuildingUI");
        if (roomBuildingUI)
        {
            textPointsValue = roomBuildingUI.GetComponentInChildren<Text>();
            textPointsValue.text=initPoints.ToString();
        }
    }

    private void pickRoom() {
        int roomId = UnityEngine.Random.Range(0, rooms.Length);
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
        movementSpeed = 100.0f;

        float deltaX = Input.GetAxis("Horizontal") * -movementSpeed;
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;
        float deltay = Input.GetAxis("Mouse ScrollWheel") * movementSpeed * 400.0f;
        
        Vector3 translationMovement = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * new Vector3(deltaX, deltay, deltaZ);
        translationMovement = Vector3.ClampMagnitude(translationMovement, movementSpeed);
        translationMovement *= Time.deltaTime;
        _charController.Move(translationMovement);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            float horizontal = Input.GetAxis("Mouse X") * 250.0f * Time.deltaTime;
            float vertical = Input.GetAxis("Mouse Y") * 250.0f * Time.deltaTime;         
            _charController.transform.RotateAround(Vector3.zero, room.transform.up, horizontal);
            _charController.transform.RotateAround(Vector3.zero, _charController.transform.right, vertical);
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
                ListItemInitRoom newItem = new ListItemInitRoom(new Vector3(hit.point.x,hit.point.y,hit.point.z), hit.transform.rotation, currentItemObject);
                UserConf.trapPositions.Add(newItem);
                instatiateTraps(newItem);
            }
        }
    }
    private void instatiateTraps(ListItemInitRoom item) {
        Vector3 position = item.position;
        position.y += 2;
        Instantiate(item.trap, position, item.rotation, GameObject.FindWithTag("Room").transform);
    }

    private void initUser() {
        List<ListItemInitRoom>  trapPositions = UserConf.trapPositions;
        string mapTitle = GameObject.Find("MapTitle").GetComponentsInChildren<Text>()[1].text;
        string userName = GameObject.Find("UserName").GetComponentsInChildren<Text>()[1].text;
        ListUser newUser = new ListUser(userId,trapPositions,room,mapTitle,userName);
        GameObject.Find("PlayerData").GetComponent<GamePlayerMng>().setUser(newUser);
        //GamePlayerMng.setUser(newUser);
        //GamePlayerMng.users.Add(newUser);
        //roomBuildingUI.GetComponentInChildren<UserIdUiBlock>().GetComponentInChildren<Text>().text=userId;
    }

    public void Ready() {
        initUser();
        SceneManager.LoadScene("Login");
    }
    void CheckState(){
        textPointsValue.text=(initPoints - GetComponent<UserConf>().getTrapPoints()).ToString();
        if (GetComponent<UserConf>().getTrapPoints() == initPoints)
        {
            readyButton.SetActive(true);
        }
    }
}
