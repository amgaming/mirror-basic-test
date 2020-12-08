using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRoomConf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UserConf.trapPositions.ForEach(item =>
        {
            Instantiate(item.trap);
            Vector3 position = item.position;
            position.y += 10;
            item.trap.transform.position = position;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
