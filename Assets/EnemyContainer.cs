using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    //passing variables that determine movement.
    public void Movement(float leftCap, float rightCap, float jumpLenght, float jumpHeight, bool facingLeft, Rigidbody2D rb, LayerMask ground, Collider2D coll)
        {
            
            {
            if (GetComponent<LocalEnemy>().facingLeft == true)
                {
                    //Test to see if we beyond the leftcap
                    if (transform.position.x > leftCap)
                    {
                        //make sure sprite faces right location
                        
                    //test to see if am on the ground, if so jump
                        if (coll.IsTouchingLayers(ground))
                        {
                            //jump
                            rb.velocity = new Vector2(-jumpLenght, jumpHeight);
                            transform.localScale = new Vector2(1, 1);
                        }
                    }
                    else
                    {
                        GetComponent<LocalEnemy>().facingLeft = false;
                    }
                }

                else
                {
                
                //Test to see if we beyond the rightCap
                    if (transform.position.x < rightCap)
                    {
                    
                    //test to see if on the ground, if so jump
                    if (coll.IsTouchingLayers(ground))
                        {
                            //jump
                            rb.velocity = new Vector2(jumpLenght, jumpHeight);
                            transform.localScale = new Vector2(-1, 1);
                        }

                    }
                    else
                    {
                    GetComponent<LocalEnemy>().facingLeft = true;
                }

                }
            }
        }


    
    public void TakeDamage(int damage)
    {
        GetComponent<LocalEnemy>().rb.velocity = Vector2.zero;
        GetComponent<LocalEnemy>().currentHP -= damage;
        GetComponent<LocalEnemy>().isHurt = true;
        Invoke("SetHurtBoolBack", 1.5f);
        //Need to add Animation >>>GetComponent<LocalEnemy>().anim.SetTrigger("hurt");//use `anim.SetBool("hurt", true)` and maybe an invoke timer locally to change boolean back

        //Play hurt animation
        if (GetComponent<LocalEnemy>().currentHP <= 0)
        {
            GetComponent<LocalEnemy>().anim.SetTrigger("death");
        }

    }
    public void Death()
    {
        Destroy(gameObject);
    }

    public void PlayerDistanceAttackTrigger(float dist, Transform player, Rigidbody2D rb, float jumpLenght)
    {
        
        //this controls the Attackswitch in NPC speed once player is in range(dist) of enemy
        if (dist < 5)
        {
            jumpLenght = jumpLenght+=5f;
            GetComponent<LocalEnemy>().attack=true;
            GetComponent<LocalEnemy>().anim.SetBool("attack", true);
        }
        else if (dist < 5 && GetComponent<LocalEnemy>().attack == true)
        {
            Invoke("SetAttackBoolBack", 10f);
            GetComponent<LocalEnemy>().anim.SetBool("attack", false);
        }
        else
        {
            GetComponent<LocalEnemy>().attack = false;
            GetComponent<LocalEnemy>().anim.SetBool("attack", false);
        } 
    }

    private void SetAttackBoolBack()
    {
        GetComponent<LocalEnemy>().attack = false;
    }
    private void SetHurtBoolBack()
    {
        GetComponent<LocalEnemy>().isHurt = false;
    }
}