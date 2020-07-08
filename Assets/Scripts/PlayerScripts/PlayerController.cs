﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement variables
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    //move input variable
    private float moveInput;

    //what we are moving
    private Rigidbody2D rb;


    //Sprite default facing direction
    private bool facingRight = true;

    //Variables for jump/doublejump conditions
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumps;

    //Animation
    private Animator anim;
    public bool isAttacking;

    //Lag after attack 1 before next attack can ocurr
    public float attackRate = 9f;
    float nextAttackTime = 0f;

    //Collider to detect NPC or item/Weapons
    private CapsuleCollider2D coll;
    [SerializeField] private float hurtForce;
    [SerializeField] private bool isHurt = false;
    public Transform attackPoint;
    
    public LayerMask enemyLayers;
    public int attackDamage = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
    }
    
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isHurt != true)
        {
            Attack();
            if (!isAttacking == true)
            {
                Movement();
                DoubleJump();
                Crouch();
            }
        }

        
    }


    private void Movement()
    {

        moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("walk", Mathf.Abs(moveInput * speed));


        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    private void Flip() //flip character model based on facingRight bool.
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void DoubleJump()
    {
        if (rb.velocity.y < .5f)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        if (isGrounded == true)
        {
            anim.SetBool("isFalling", false);
            extraJumps = 1;
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            anim.SetBool("isJumping", true); //I think this is where the double jump animation bool belongs
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("isJumping", true);
        }
    }
    private void Crouch()
    {
        if (Input.GetAxisRaw("Vertical") < -.5 && isGrounded == true)
        {
            //need to manipulate hurt box here as well
            anim.SetBool("isCrouching", true);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }
    }

    private void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                //enabling hitbox
                gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
                //bool for animation/movement disablement based on attack
                isAttacking = true;
                //animation
                anim.SetTrigger("attack");
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", false);
                Invoke("SetAttackBoolBack", .75f);

                //hit detection
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 1f , enemyLayers);
                //damage
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("WE HIT " + enemy.name);
                    enemy.GetComponent<LocalEnemy>().TakeDamage(attackDamage);
                }
                //hitlag
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                //enabling hitbox
                gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
                //bool for animation/movement disablement based on attack
                isAttacking = true;
                //animation
                anim.SetTrigger("attack2");
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", false);
                Invoke("SetAttackBoolBack", .75f);
                
                //hit detection
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 1.25f, enemyLayers);
                //damage
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("WE HIT " + enemy.name);
                    enemy.GetComponent<LocalEnemy>().TakeDamage(attackDamage);
                }
                //hitlag
                nextAttackTime = Time.time + 1.5f / attackRate;
            }
            if (isGrounded == true)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    //enabling hitbox
                    gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
                    //bool for animation/movement disablement based on attack
                    isAttacking = true;
                    //animation
                    anim.SetTrigger("crouchAttack");
                    anim.SetBool("isCrouching", false);
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isFalling", false);
                    Invoke("SetAttackBoolBack", .75f);
                   
                    //hit detection
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 1f, enemyLayers);
                    //damage
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Debug.Log("WE HIT " + enemy.name);
                        enemy.GetComponent<LocalEnemy>().TakeDamage(attackDamage);
                    }
                    //hitlag
                    nextAttackTime = Time.time + .75f / attackRate;
                }
            }
        }
    }

    private void SetAttackBoolBack()
    {
        isAttacking = false;
        //disabling hitbox
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
        //print("unlocked");
    }

    void OnDrawGizmosSelected()//visual scene reference for circumfrence of ground detection
    {

        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
   
    }

    private void OnCollisionEnter2D(Collision2D other)///Need to look at this section of the code to fix the animation for the hurt box when not facing enemy.
    {
        if (other.gameObject.tag == "Enemy")
        {
            LocalEnemy enemy = other.gameObject.GetComponent<LocalEnemy>();
            isHurt = true;
            Invoke("SetHurtBoolBack", .75f);
            anim.SetTrigger("hurt");
            anim.SetBool("isCrouching", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            if (enemy.transform.position.x > transform.position.x)
            {
                //enemy is to player's right, if damaged to player ocurrs player is pushed left
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else
            {
                //enemy is to player's left, if damaged to player ocurrs player is pushed right
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
        }

    }
    private void SetHurtBoolBack()
    {
        isHurt = false;
    }

}
