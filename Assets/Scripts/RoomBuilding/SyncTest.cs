using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncTest : NetworkBehaviour
{
    // Start is called before the first frame update
    [SyncVar]
    private NetworkIdentity _userDataData;

    public override void OnStartAuthority() {
        base.OnStartAuthority();
        Debug.Log("OnStartAuthority");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
