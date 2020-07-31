using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public float input1;
    public float input2;
    public float input3;
    public Transform firePoint;
     [SerializeField] private bool fireTomes = false;
    [SerializeField] private bool lighteningTomes = false;
    public string recentTome;
    [SerializeField] public float timer;
    public GameObject flamePrefab;
    public GameObject ligtheningPrefab;
    public GameObject chainPrefab;
    public bool inputOrder1 = false;
    public bool inputOrder2 = false;
    public float maxHp;
    public float currentHp;
    [SerializeField] public int maxMp = 500;
    public int currentMp;
    public int tomeMpCost;
    MagicMonitor currentMagicState;
    private float spellInputVertical;
    private float spellInputHorizontal;
    public GameObject lighteningTomeSlot;
    public GameObject fireTomeSlot;
    public string lastDpadInput;
    //Lag after attack 1 before next attack can ocurr
    public float attackRate = 9f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentMp = maxMp;
        currentMagicState = GameObject.Find("MagicAnimeContainer").GetComponent<MagicMonitor>();
    }

    // Update is called once per frame
    void Update()
    {
        hadouken();
        if (Input.anyKeyDown)
        {
            StartCoroutine(ResetInputTimes());
        }
        maxHp = GetComponent<PlayerController>().maxHp;
        currentHp = GetComponent<PlayerController>().currentHp;
        GetTome();
        GetSetMagicAnimeContainer();
        LocalSpellController();

    }
 

    public void hadouken()
    {

        if (Input.GetAxisRaw("Vertical") < -.5)
        {
            input1 = Time.time;
        }
       
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > .5)
        {
            input2 = Time.time;
   
        }
       
        if (Input.GetButtonDown("Fire3"))
        {
            input3 = Time.time;
        }
        if (input3 - input2 <= .5 && input2 - input1 <= .5 && input3 - input1 <= .7  && input1 != 0 && input2 != 0 && input3 != 0)
        {
           
            input1 = 0;
            input2 = 0;
            input3 = 0;
            if (Time.time >= nextAttackTime)
               
            {
                
                print("HADOUKEN");
                Shoot();
                //hitlag
                nextAttackTime = Time.time + .75f / attackRate;
            }
        }
     }

    IEnumerator ResetInputTimes()
    {
        yield return new WaitForSeconds(.075f);
        ResetInputs();
    }

    public void ResetInputs()
    {
        input1 = 0;
        input2 = 0;
        input3 = 0;
    }
    void Shoot()
    {
       
            if (fireTomes == true && tomeMpCost <= currentMp && lastDpadInput == "RIGHT")
            {

            
                tomeMpCost = 25;
                if (currentHp == maxHp)
                {

                    currentMp -= tomeMpCost;
                    //instantiate projectile
                    Instantiate(flamePrefab, firePoint.position, firePoint.rotation);
                    Vector3 fp = firePoint.position;
                    fp.y += 1f;
                    Instantiate(flamePrefab, fp, firePoint.rotation);
                    Vector3 fp1 = firePoint.position;
                    fp1.y -= 1f;
                    Instantiate(flamePrefab, fp1, firePoint.rotation);
                }
                else
                {
                    //instantiate projectile
                    Instantiate(flamePrefab, firePoint.position, firePoint.rotation);
                currentMp -= tomeMpCost;
            }
            }
            if (lighteningTomes == true && tomeMpCost <= currentMp && lastDpadInput == "UP")
            {
               
                tomeMpCost = 25;
                if (currentHp == maxHp)
                {
                    currentMp -= tomeMpCost;
                    //instantiate projectil
                    Instantiate(ligtheningPrefab, firePoint.position, firePoint.rotation);
                    Vector3 fp = firePoint.position;
                    fp.y += 1f;
                    Instantiate(ligtheningPrefab, fp, firePoint.rotation);
                    Vector3 fp1 = firePoint.position;
                    fp1.y -= 1f;
                    Instantiate(ligtheningPrefab, fp1, firePoint.rotation);

                }
                else
                {
                    //instantiate projectile
                    Instantiate(ligtheningPrefab, firePoint.position, firePoint.rotation);
                currentMp -= tomeMpCost;
            }
            }
            //This Garbage was an attempt at generating multiple prefabs in every direction outward from player center
            /*else
            {
                Instantiate(chainPrefab, new Vector3(firePoint.position.x, (firePoint.position.y - .5f)), firePoint.rotation);
                Instantiate(chainPrefab, new Vector3(firePoint.position.x, (firePoint.position.y + .5f)), firePoint.rotation);
                Instantiate(chainPrefab, firePoint.position, firePoint.rotation);
                *//*  Instantiate(chainPrefab, new Vector3(firePoint.position.x, (firePoint.position.y + 2.5f)), Quaternion.Euler(new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z + 66))); 
                  Instantiate(chainPrefab, new Vector3(firePoint.position.x, (firePoint.position.y + 1.5f)), Quaternion.Euler(new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z + 33)));*//*
            }*/
        
       
    }
    void GetTome()
    {
       recentTome = GetComponent<PlayerController>().itemName;
        if(recentTome == "fireTome")
        {
           fireTomes = true;
        }
        if(recentTome == "lighteningTome")
        {
            lighteningTomes = true;
        }
      
    }

    void GetSetMagicAnimeContainer()
    {
      
        if (currentMp == maxMp)
        {
            currentMagicState.currentMagicBar = MagicMonitor.magicBar.full;

        }
        if(currentMp <= (maxMp * .75))
        {
            currentMagicState.currentMagicBar = MagicMonitor.magicBar.threeQ;
        }
        if (currentMp <= (maxMp / 2))
        {
            currentMagicState.currentMagicBar = MagicMonitor.magicBar.half;
        }
        if (currentMp <= (maxMp * .25))
        {
            currentMagicState.currentMagicBar = MagicMonitor.magicBar.oneQ;
        }
        if ( currentMp <= 0)
        {
            currentMagicState.currentMagicBar = MagicMonitor.magicBar.empty;
        }
    }
    void LocalSpellController()
    {
        spellInputVertical = Input.GetAxisRaw("Debug Vertical");
        spellInputHorizontal = Input.GetAxisRaw("Debug Horizontal");
        if (spellInputVertical < -.5f)
        {
            print("THIS IS DOWN ON DPAD");
            lastDpadInput = "DOWN";
            fireTomeSlot.GetComponent<ParticleSystem>().Stop();
            lighteningTomeSlot.GetComponent<ParticleSystem>().Stop();
        }
        if (spellInputVertical > .5f)
        {
            print("This is up on dpad");
            lastDpadInput = "UP";
            lighteningTomeSlot.GetComponent<ParticleSystem>().Play();
            fireTomeSlot.GetComponent<ParticleSystem>().Stop();
        }
        if(spellInputHorizontal < -.5f)
        {
            print("left on dpad");
            lastDpadInput = "LEFT";
            fireTomeSlot.GetComponent<ParticleSystem>().Stop();
            lighteningTomeSlot.GetComponent<ParticleSystem>().Stop();
        }
        if (spellInputHorizontal > .5f)
        {
            print("right on dpad");
            lastDpadInput = "RIGHT";
            lighteningTomeSlot.GetComponent<ParticleSystem>().Stop();
            fireTomeSlot.GetComponent<ParticleSystem>().Play();
        }
    }
}

