using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserConf : MonoBehaviour
{
    static public List<Transform> trapPositions;
    // Start is called before the first frame update
    void Start()
    {

    }

    static public void setTrapPositions(List<Transform> data)
    {
        trapPositions = data;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
