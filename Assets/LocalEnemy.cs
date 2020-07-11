using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LocalEnemy : EnemyContainer
{
    
    //Enviornment/Physics anchors
    public Rigidbody2D rb;
    public Collider2D coll;

    //disable movement
    public bool isHurt;
    //boundaries for npc movement
    [SerializeField] public float leftCap;
    [SerializeField] public float rightCap;
    [SerializeField] public int leftCapMultiplier;
    [SerializeField] public int rightCapMultiplier;

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
    [SerializeField] private int maxHP;
    [SerializeField] public int damage;
    public int currentHP;

    //projectile vars
    public Transform firePoint;
    public GameObject fireballPrefab;
    public bool isCreated;

    //Enemy Type Toggles
    [SerializeField] private bool canFly;
    [SerializeField] private bool canWalk;
    [SerializeField] private bool hasProjectile;
    [SerializeField] private bool isBoss;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        currentHP = maxHP;
        leftCap = rb.position.x - leftCapMultiplier;
        rightCap = rb.position.x + rightCapMultiplier;
    }

    // Update is called once per frame

    protected virtual void Update()
    {
        dist = Vector2.Distance(player.position, rb.position);
        if (!isHurt)
        {
            if (hasProjectile)
            {
                Movement(leftCap, rightCap, jumpLenght, jumpHeight, facingLeft, rb, ground, coll);
                
                if (dist < 7)
                {
                    Shoot();
                }
                
            }
            if (canFly)
            {
                AirMovement(leftCap, rightCap, jumpLenght, jumpHeight, facingLeft, rb, coll);
                //cool flying logic inbound A* pathfinding
            }
            else
            {
                Movement(leftCap, rightCap, jumpLenght, jumpHeight, facingLeft, rb, ground, coll);
                PlayerDistanceAttackTrigger(dist, player, rb, jumpLenght);
            }
        }
        
    }

    public void Shoot()
    {
        if (!isCreated)
        {
            anim.SetBool("attack", true);
            attack = true;
           
            Invoke("SetIsCreatedBoolBack", 5f);
            if (anim.GetBool("attack") == true)
            {
                Invoke("CreateFireball", 1f);
                isCreated = true;
            }
        }
    }
    private void SetIsCreatedBoolBack()
    {
        isCreated = false;
        anim.SetBool("attack", false);
        attack = false;
    }
    public void CreateFireball()
    {
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }
}
