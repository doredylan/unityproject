using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform firePoint;
    public GameObject knifePrefab;

    // Update is called once per frame
    void Update()
    {
      /*if(Input.GetButton("Mouse X"))
        {
            Shoot();
        }*/
    }


    void Shoot()
    {
        //shooting logic
       // Instantiate(knifePrefab, firePoint.position, firePoint.rotation);
    }
}
