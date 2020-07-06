using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : EnemyContainer
{

    public int enemyCurrentHp;

    public void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHp = GameObject.Find("EnemyContainer").GetComponent<EnemyHP>().currentHp -= damage;
       
       
        
        //Play hurt animation
        if (enemyCurrentHp <= 0)
        {
            Death();
            //Maybe add animation trigger here....
        }

    }

}
