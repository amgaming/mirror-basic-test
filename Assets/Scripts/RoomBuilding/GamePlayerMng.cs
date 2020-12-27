using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItemInitRoom 
{

    public Vector3 position;   
    public Quaternion rotation;   
    public GameObject trap;   
    public ListItemInitRoom(Vector3 val1, Quaternion val2, GameObject val3) {
        position = val1;
        rotation = val2;
        trap = val3;
    }
}


public class GamePlayerMng : MonoBehaviour
{
    static public List<ListItemInitRoom> trapPositions = new List<ListItemInitRoom>();    
    // Start is called before the first frame update
    void Start()
    {

    }

    static public void setTrapPositions(List<ListItemInitRoom> data)
    {
        trapPositions = data;
    }

    public int getTrapPoints() {
        int points = 0;
        trapPositions.ForEach(item =>
        {
            points += item.trap.GetComponent<ItemTrap>().price;
        });
        return points;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
