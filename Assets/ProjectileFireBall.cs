using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFireBall : MonoBehaviour
{
   
    public Transform firePoint;
    public GameObject FireBall;
    public bool isShooting;

        // Update is called once per frame
        void Update()
        {
        isShooting = GetComponent<LocalEnemy>().anim.GetBool("attack") == true;
            if (isShooting == true)
            {
                Shoot();
            }
        }


        void Shoot()
        {
            //shooting logic
            Instantiate(FireBall, firePoint.position, firePoint.rotation);
        }
 
}
