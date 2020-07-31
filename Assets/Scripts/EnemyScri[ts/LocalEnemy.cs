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
    public GameObject chainSpellPrefab;
    public bool isCreated;
    [SerializeField] private int numofAttacksPerTurn = 20;

    //Enemy Type Toggles
    [SerializeField] private bool canFly;
    [SerializeField] private bool canWalk;
    [SerializeField] private bool hasProjectile;
    [SerializeField] private bool isBoss;
    public GameObject GeneratedWall;


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
            if (!isBoss)
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
            if (isBoss)
            {
                if(currentHP > (maxHP / 2))
                {
                    if (dist < 9)
                    {
                         
                        GeneratedWall.SetActive(true);
                        Shoot();
                    }

                }
                if(currentHP <= (maxHP / 2))
                {
                    if (dist < 20)
                    {
                        Shoot2();
                    }
                }
            }
        }
        
    }

    public void Shoot()
    {
        if (!isCreated)
        {
            anim.SetBool("attack", true);
            attack = true;
           
            Invoke("SetIsCreatedBoolBack", 2.5f);
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
        Vector3 fp = firePoint.position;
        fp.y -= 2f;
        Instantiate(fireballPrefab, fp, firePoint.rotation);
    }
    public void CreateChainSpell()
    {
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
       
        for (int i = 0; i < numofAttacksPerTurn; i++)
        {
            Vector2 position = new Vector2(Random.Range(-725.0f, -705.0f), Random.Range(729.0f, 739.0f));
            Instantiate(chainSpellPrefab, position, firePoint.rotation);
        }
         
    }
    public void Shoot2()
    {
        if (!isCreated)
        {
            anim.SetBool("attack", true);
            attack = true;

            Invoke("SetIsCreatedBoolBack", 3f);
            if (anim.GetBool("attack") == true)
            {
                Invoke("CreateChainSpell", 1f);
                isCreated = true;
            }
        }
    }
    
    public void FindTarg()
    {
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
