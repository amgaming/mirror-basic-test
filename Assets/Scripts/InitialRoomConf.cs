using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRoomConf : MonoBehaviour
{
    public GameObject Trap001;
    // Start is called before the first frame update
    void Start()
    {
        UserConf.trapPositions.ForEach(position =>
        {
            Instantiate(Trap001, new Vector3(position.x, -4, position.z), Quaternion.identity, transform);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
