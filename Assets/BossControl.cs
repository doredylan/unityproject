using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public int maxHp = 200;
    int currentHp;
    [SerializeField] private float jumpHeight = 3f;
    //Enviornment/Physics anchors
    [SerializeField] private LayerMask ground;
    private Rigidbody2D rb;
    private Collider2D coll;
    public Transform player;
    public float dist;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(player.position, rb.position);
        BossMovement();
        attackTrigger();
        Invoke("SetBoolBack", 5f);
        //if (anim.GetBool("bossDeath") != true)
        //{
        //    //BossMovement();
        //    //attackTrigger();
        //    //Invoke("SetBoolBack", 1f);
        //}
    }

    private void BossMovement()
    {
        if (coll.IsTouchingLayers(ground))
        {
            //jump
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }



    //public void NPCDeathTrigger()
    //{
    //    anim.SetTrigger("npcdeath");
    //}

    public void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        print(currentHp);
        //Play hurt animation
        if (currentHp <= 0)
        {
            Death();
            //NPCDeathTrigger();
        }

    }

    private void attackTrigger()
    {
        if (dist < 10f)
        {
            //instantiate a projectile
            anim.SetBool("BossAttack", true);
        }
           // print(anim.GetBool("BossAttack"));

    }
    private void SetBoolBack()
    {
        anim.SetBool("BossAttack", false);
    }
}
