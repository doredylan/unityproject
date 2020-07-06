using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public LayerMask Player;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        NewPlayerControl player = hitInfo.GetComponent<NewPlayerControl>();
        if (player != null)
        {
            Debug.Log("The fireball hit " + hitInfo);
            player.TakeDamage(damage);
           
        }
       



        Destroy(gameObject);

    }

}
