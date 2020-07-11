using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 20f;
    [SerializeField] public int damage = 40;
    public Rigidbody2D rb;
    public LayerMask Player;
    //public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }



    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
           
                player.GetComponent<PlayerController>().takeDamage(damage);
                 //Debug.Log("The fireball hit " + hitInfo);
            
            // Instantiate(impactEffect, transform.position, transform.rotation);
        }

        //  Debug.Log("The fireball hit " + hitInfo);


        Destroy(gameObject);

    }

}
