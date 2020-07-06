using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public LayerMask enemyLayers;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        LocalEnemy enemy = hitInfo.GetComponent<LocalEnemy>();
        if (enemy != null)
        {
            Debug.Log("The Knife hit " + hitInfo);
            enemy.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        
       
 
        
        Destroy(gameObject);
        
    }
}
