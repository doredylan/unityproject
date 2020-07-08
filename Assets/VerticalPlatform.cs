using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{

    private PlatformEffector2D effector;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }
    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxisRaw("Vertical") < -.5)
        {
            //effector.rotationalOffset = 180f;
             effector.colliderMask = 0;
        }
        else if (Input.GetAxisRaw("Vertical") >= 0)
        {
            //effector.rotationalOffset = 0f;

            effector.colliderMask = 1024;

           
        }
    }
}

