using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int maxHp = 100;
    int currentHp;

    //private GameObject npc;
    //boundaries for npc movement
    [SerializeField] public float leftCap;
    [SerializeField] public float rightCap;
    
    //distance vars for npc movment
    [SerializeField] private float jumpLenght = 3f;
    [SerializeField] private float jumpHeight = 3f;

    //Enviornment/Physics anchors
    [SerializeField] private LayerMask ground;
    private Rigidbody2D npcRb;
    private Collider2D npcColl;
    public Transform player;

 

    //Animation anchors
    private bool facingLeft = true;
    private bool dash;
    private Animator anim;
    public float dist;
    //public bool npcdeath = false;
    [SerializeField] public bool isHurt = false;


    private void Start()
    {
        currentHp = maxHp;
        npcRb = GetComponent<Rigidbody2D>();
        npcColl = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
       // npc = GetComponent<GameObject>();
    }

    private void Update()
    {



        if (isHurt != true)
        {
            NPCMovement();
            if (anim.GetBool("npcdeath") != true)
            {
                NPCMovement();
                dist = Vector2.Distance(player.position, npcRb.position);
                FindPlayersDistance();
                anim.SetBool("dash", (bool)dash);

            }
        }




            //print(currentHp + " hp for npc");

        }

    private void NPCMovement()
    {
        if (facingLeft)
        {
            //Test to see if we beyond the leftcap
            if (transform.position.x > leftCap)
            {
                //make sure sprite faces right location

                //test to see if am on the ground, if so jump
                if (npcColl.IsTouchingLayers(ground))
                {
                    //jump
                    npcRb.velocity = new Vector2(-jumpLenght, jumpHeight);
                    transform.localScale = new Vector2(1, 1);
                }
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {
            //Test to see if we beyond the rightCap
            if (transform.position.x < rightCap)
            {

                //test to see if on the ground, if so jump
                if (npcColl.IsTouchingLayers(ground))
                {
                    //jump
                    npcRb.velocity = new Vector2(jumpLenght, jumpHeight);
                    transform.localScale = new Vector2(-1, 1);
                }

            }
            else
            {
                facingLeft = true;
            }

        }
    } 

  


    private void FindPlayersDistance()
    {
     //this controls the switch in NPC speeed & anim once player is in range(dist) of enemy
        if (dist < 5)
        {
            jumpLenght = 5f;
            dash = true;

        }
        else
        {
            dash = false;
            jumpLenght = 3f;
        }
    }

    public void NPCDeathTrigger()
    {
        anim.SetTrigger("npcdeath");
        
    }

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log(isHurt);
        isHurt = true;
        Invoke("SetBoolBack", 30f);
        //Play hurt animation
        if (currentHp <= 0)
        {
            NPCDeathTrigger();
            Death();
        }

    }
    private void SetBoolBack()
    {
        isHurt = false;
    }
}