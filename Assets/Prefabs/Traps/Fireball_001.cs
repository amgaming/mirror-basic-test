using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Fireball_001 : NetworkBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;
    private Trap_002 trap;

    void Update() {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other) {    
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null) {    
            trap.hit();
        }
        Destroy(this.gameObject);
    }

    public void SetTrap(Trap_002 val) {
        trap = val;
    }
}
