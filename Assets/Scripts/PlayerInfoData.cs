using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoData : MonoBehaviour
{
    public ListUser user;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUser(ListUser data)
    {
        user = data;
    }

    public ListUser getUser()
    {
        return user;
    }
}
