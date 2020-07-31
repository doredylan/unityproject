using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpeed : MonoBehaviour
{
    public float fallMultiplier = 1.5f;
    public float fastFallMultiplier = 1.75f;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            if(rb.velocity.y < 0 && Input.GetAxisRaw("Vertical") < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fastFallMultiplier - 1) * Time.deltaTime;
            }
        }
    }
}
