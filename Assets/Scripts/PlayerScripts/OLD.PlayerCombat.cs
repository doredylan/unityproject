﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator Animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = .05f;
    public int attackDamage = 100;
    public float attackRate = 4f;
    float nextAttackTime = 0f;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime) {
            //print(Physics2D.OverlapArea(SwordHitBox.transform.position, enemy.transform.position));
            if (Input.GetButtonDown("Fire1") && GetComponent<NewPlayerControl>().isCrouching == false)
             {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetButtonDown("Fire2") && GetComponent<NewPlayerControl>().isCrouching == false)
            {
                Attack2();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetButtonDown("Fire1") && GetComponent<NewPlayerControl>().isCrouching == true)
            {
                CrouchAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
       /* if (Input.GetKeyDown(KeyCode.R))
        {
            print(Animator.GetCurrentAnimatorStateInfo(0).length + Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }*/
    }

    private void Attack()
    {
        //animation
        Animator.SetTrigger("attack");
       

        //hit detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage
        
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("WE HIT " + enemy.name);
            enemy.GetComponent<LocalEnemy>().TakeDamage(attackDamage);
            }
      
    }

    private void Attack2()
    {
        //animation
        Animator.SetTrigger("attack2");
    
        //hit detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage

        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("WE HIT " + enemy.name);
            enemy.GetComponent<LocalEnemy>().TakeDamage(attackDamage);
        }

    }
    private void CrouchAttack()
    {
        //animation
        Animator.SetBool("crouchAttack", true);
       
        //hit detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage

        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("WE HIT " + enemy.name);
            enemy.GetComponent<LocalEnemy>().TakeDamage(attackDamage);
        }

    }


    //void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;

    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}


    IEnumerator WaitForAttackAnimation()
    {
        // suspend execution till attack anim completes
        Attack();
        yield return new WaitForSeconds(3);
        print("WaitForAttackAnimation " + Time.time);
    }

    IEnumerator MyCorouttine()
    {
        print("Starting " + Time.time);

        // Start function WaitForAttackAnimation as a coroutine
        yield return StartCoroutine("WaitForAttackAnimation");
        print("Done " + Time.time);
    }



}
