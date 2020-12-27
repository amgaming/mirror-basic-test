using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = 5;    
    }

    public void Hurt(int damage)
    {
        _health -= damage;
        Debug.Log("Health: " + _health);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("aqui ---------------------");
        Debug.Log(SceneManager.GetActiveScene().name);
        Debug.Log("aqui ---------------------");
    }
}
