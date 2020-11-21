using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRoomConf : MonoBehaviour
{
    public GameObject Trap001;
    // Start is called before the first frame update
    void Start()
    {
        UserConf.trapPositions.ForEach(trapTransform =>
        {
            GameObject p = Instantiate(Trap001, Vector3.zero, Quaternion.identity);
            //p.transform.position = pos;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
