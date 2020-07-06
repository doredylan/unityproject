using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPlayerControl : MonoBehaviour
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
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumps;

    //variables for wallsliding
    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    public float checkFRadius;

    //variables forr walljumping
    bool wallJumping;
    public float xWallJumpForce;
    public float yWallJumpForce;
    public float wallJumpTime;
    public int numOfWallJumps;

    //Animation
    private Animator anim;

    //Collider to detect NPC or item/Weapons
    private CapsuleCollider2D coll;
    [SerializeField] private float hurtForce;
    [SerializeField] private bool isHurt = false;

    //HP variables
    public int maxHp = 1000;
    public int currentHp;
    public HealthBar healthBar;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
        currentHp = maxHp;
       // healthBar.SetMaxHealth(maxHp);
        
    }

    private void FixedUpdate() //This runs multiple times per frame based on project settings
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
        if (isHurt != true)
        {
            Movement();
        }
    }

    private void Update()//This runs once per frame
    {
       // OnDrawGizmosSelected();
        if (isHurt != true)
        {
            DoubleJumpAndWallSlide();
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
    private void DoubleJumpAndWallSlide()
    {
        if(rb.velocity.y < .1f && wallSliding == false)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
            anim.SetBool("isWallsliding", false);
            
        }
        if (isGrounded == true)
        {
            anim.SetBool("isFalling", false);
            extraJumps = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            anim.SetBool("isJumping", true); //I think this is where the double jump animation bool belongs
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("isJumping", true);
        }

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkFRadius, whatIsGround);
        if (isTouchingFront == true && isGrounded == false)
        {
            extraJumps = 0; //enabling this will give a double jump afte rwallcling
            wallSliding = true;
         
            anim.SetBool("isWallsliding", true);
            anim.SetBool("isFalling", false);
            anim.SetBool("isJumping", false);
        }
        else
        {
            wallSliding = false;
           
            anim.SetBool("isWallsliding", false);
        }
        if (wallSliding == true)
        {
            anim.SetBool("isWallsliding", true);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
           
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
            //WallJumping animation goes here
        }
        if (wallJumping == true)
        {
            
            rb.velocity = new Vector2(xWallJumpForce * -moveInput, yWallJumpForce);
            //(OR)WallJumping animation goes here 
        }
        
    }

    private void Flip() //flip character model based on facingRight bool.
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }


    void OnDrawGizmosSelected()//visual scene reference for circumfrence of ground detection
    {

        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(frontCheck.position, checkFRadius);
    }



    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    public void TakeDamage(int damage)
    {
        
        currentHp -= damage;
        healthBar.SetHealth(currentHp);
        
        //Play hurt animation
        if (currentHp <= 0)
        {
           // Debug.Log(gameObject + "is dead");
            //Create death animation for player 1
            //Destroy(gameObject);
            SceneManager.LoadScene("GameOver");

        }
        else        {
            
            // anim.SetTrigger("hurt");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)///Need to look at this section of the code to fix the animation for the hurt box when not facing enemy.
    {
        if (other.gameObject.tag == "Enemy")
        {
            NPC npc = other.gameObject.GetComponent<NPC>();
            isHurt = true;
            Invoke("SetBoolBack", 1f);
            anim.SetTrigger("hurt");
            if (other.gameObject.transform.position.x > transform.position.x)
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
    private void SetBoolBack()
    {
        isHurt = false;
    }


  

}


//OLD COLLISION HURTFORCE CODE
//private void OnCollisionEnter2D(Collision2D other)//THIS NEEDS TO BE REWORKED IT IS BUGGY//Need to look at this section of the code to fix the animation for the hurt box when not facing enemy.
//{
//    if (other.gameObject.tag == "Enemy")
//    {
//        NPC npc = other.gameObject.GetComponent<NPC>();
//        state = State.hurt;
//        if (other.gameObject.transform.position.x > transform.position.x)
//        {
//            //enemy is to player's right, if damaged to player ocurrs player is pushed left
//            rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
//        }
//        else
//        {
//            //enemy is to player's left, if damaged to player ocurrs player is pushed right
//            rb.velocity = new Vector2(hurtForce, rb.velocity.y);
//        }
//    }

//}


//OLD ANIMATION CODE
//private void AnimationSwitch()//Sets Animation State
//{
//    if (state == State.Jump)
//    {
//        if (rb.velocity.y < .01f)
//        {
//            state = State.falling;
//        }
//    }
//    else if (state == State.falling)
//    {
//        if (isGrounded == true && rb.position.y == rb.position.y)
//        {
//            state = State.idle;
//        }
//    }
//    else if (state == State.hurt)
//    {
//        if (Mathf.Abs(rb.velocity.x) < .01f && rb.position.y == rb.position.y)
//        {
//            state = State.idle;
//        }
//    }
//    else if (Mathf.Abs(rb.velocity.x) > 1.5f && isGrounded == true)
//    {
//        //moving
//        state = State.Walk;
//    }
//    //For expanind idle animation with weapons or other objects
//    //else if (sword > 0)
//    //{
//    //    state = State.armed;
//    //}
//    else if (wallSliding == true)
//    {
//        state = State.wallslide;

//    }

//    else
//    {
//        state = State.idle;
//    }
//}
