﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : EnemyContainer
{

    [SerializeField] public int maxHp;
    [SerializeField] public int currentHp;

    

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;  
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
