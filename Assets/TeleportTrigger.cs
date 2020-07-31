using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public enum TeleportPoints { L, R, C };
    public TeleportPoints teleportPoint;

    [SerializeField] Transform L;
    [SerializeField] Transform R;
    [SerializeField] Transform C;
    [SerializeField] public bool dongle;
    [SerializeField] public bool facingLeft = true;


    private void Start()
    {

    }

    void Update()
    {
        dongle = GetComponent<LocalEnemy>().isHurt;

        if (dongle == true)
        {
            teleportPoint = (TeleportPoints)Random.Range(0, 2);
            if (teleportPoint == TeleportPoints.L)
            {
                transform.position = L.position;
                facingLeft = false;
                Flip();

            }
            if (teleportPoint == TeleportPoints.R)
            {
                transform.position = R.position;
                facingLeft = true;

            }
            if (teleportPoint == TeleportPoints.C)
            {
                transform.position = C.position;

            }

        }
       
    }


    private void Flip() //flip character model based on facingRight bool.
    {
        if (facingLeft == false)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }

}


