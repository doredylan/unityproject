using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEnemy : EnemyContainer
{
    
    //Enviornment/Physics anchors
    public Rigidbody2D rb;
    public Collider2D coll;
    
    //boundaries for npc movement
    [SerializeField] public float leftCap;
    [SerializeField] public float rightCap;

    //distance vars for npc movment
    [SerializeField] public float jumpLenght = 3f;
    [SerializeField] public float jumpHeight = 3f;
    public bool facingLeft = true;
    public LayerMask ground;

    //Attack Triggers
    public bool attack;
    public float dist;
    public Transform player;

    //Animation control
    public Animator anim;

    //Enemy Stats
    public int maxHP = 100;
    public int currentHP;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        currentHP = maxHP;
        leftCap = rb.position.x - 5;
        rightCap = rb.position.x + 5;
    }

    // Update is called once per frame

    protected virtual void Update()
    {
        dist = Vector2.Distance(player.position, rb.position);
        Movement(leftCap, rightCap,jumpLenght, jumpHeight, facingLeft, rb, ground, coll);
        PlayerDistanceAttackTrigger(dist, player, rb, jumpLenght);
    }
    
}
