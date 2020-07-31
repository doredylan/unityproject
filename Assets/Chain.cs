using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    
    public LayerMask Player;
    public int damage = 150;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * speed;
        StartCoroutine(DeleteThis());
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


       

    }
    IEnumerator DeleteThis()
    {
        yield return new WaitForSeconds(.85f);
        Destroy(gameObject);
    }
}
