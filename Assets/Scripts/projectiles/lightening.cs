using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightening : MonoBehaviour
{

    public float speed = 0.25f;
    public LayerMask enemyLayers;
    public int damage = 150;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        StartCoroutine(DeleteThis());
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        LocalEnemy enemy = hitInfo.GetComponent<LocalEnemy>();
        if (enemy != null)
        {

            enemy.TakeDamage(damage);

        }


    }
    IEnumerator DeleteThis()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }
}
