using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCombat : MonoBehaviour
{
    public Animator anime;
    public Transform attackPoint;
    public LayerMask Player;
    public float attackRange = .05f;
    public int attackDamage = 1;


    //New Timer logic, maybe make this a coroutine later
    float _hitTime = 10;
    float _hitTimer = 0;
    bool _canHit = true;

    // Update is called once per frame
    void Update()
    {
    /*    Attack();

        // Increment the hit timer
     _hitTimer += Time.deltaTime;


        if (_hitTimer > _hitTime)
        { _canHit = true; }

        //print(Physics2D.OverlapArea(SwordHitBox.transform.position, enemy.transform.position));
        //int rnd = Random.Range(0, 3);
        //if (rnd == 0)
        //{
        //    Attack();
        //}

*/

    }

    //private void Attack()
    //{
    //    //animation
    //    //anime.SetTrigger("dash");

    //    //hit detection
    //    Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Player);

    //    //damage
        


    //    foreach (Collider2D player in hitPlayer)
    //    {
    //        //Debug.Log("We hit" + player.name + "for" + attackDamage);
    //        player.GetComponent<NewPlayerControl>().TakeDamage(attackDamage);
    //    }
    //}

    void Attack()
    {
        // If can't be hit yet, return
        if (!_canHit)
            return;

        // Code here to apply damage
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Player);

        //damage



        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("We hit " + player.name + " for " + attackDamage);
            player.GetComponent<NewPlayerControl>().TakeDamage(attackDamage);
        }
        // Reset the hit timer when taking damage
        _hitTimer = 0;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
