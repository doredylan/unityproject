using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrucibleWall : MonoBehaviour
{
    public bool attack;
    public LayerMask Player;
    public GameObject Boss;
    public GameObject GeneratedWall;
    private void Update()
    {
        if (!Boss)
        {
            Destroy(GeneratedWall);
        }
        //  attack = GetComponent<PlayerController>().isAttacking;
    }

    private void OnTriggerEnter2D(Collider2D Player)
    {

        if (!Boss)
        {
            
            if (Player != null)
            {
                Destroy(gameObject);
            }
        }
      


      

    }
}
