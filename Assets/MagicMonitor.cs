using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class MagicMonitor : MonoBehaviour
{
    public enum magicBar { full, threeQ, half, oneQ, empty}; // Start is called before the first frame update
    public magicBar currentMagicBar;


    void Start()
    {
        currentMagicBar = magicBar.full;
       
    }

    // Update is called once per frame
    void Update()
    {
     
      if(currentMagicBar == magicBar.full)
        {
            FullBar();
        }
      if(currentMagicBar == magicBar.threeQ)
        {
            ThreeQBar();
        }
      if(currentMagicBar == magicBar.half)
        {
            HalfBar();
        }
      if(currentMagicBar == magicBar.oneQ)
        {
            OneBar();
        }
      if(currentMagicBar == magicBar.empty)
        {
            EmptyBar();
        }
    }
    void FullBar()
    {
       GameObject.Find("Flame0").GetComponent<SpriteRenderer>().enabled = true;
       GameObject.Find("Flame1").GetComponent<SpriteRenderer>().enabled = true;
       GameObject.Find("Flame2").GetComponent<SpriteRenderer>().enabled = true;
       GameObject.Find("Flame3").GetComponent<SpriteRenderer>().enabled = true;

    }
    void ThreeQBar()
    {
        GameObject.Find("Flame0").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Flame1").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Flame2").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Flame3").GetComponent<SpriteRenderer>().enabled = false;
    }
    void HalfBar()
    {
        GameObject.Find("Flame0").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Flame1").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Flame2").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Flame3").GetComponent<SpriteRenderer>().enabled = false;
    }
    void OneBar()
    {
        GameObject.Find("Flame0").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Flame1").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Flame2").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Flame3").GetComponent<SpriteRenderer>().enabled = false;
    }
    void EmptyBar()
    {
        GameObject.Find("Flame0").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Flame1").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Flame2").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Flame3").GetComponent<SpriteRenderer>().enabled = false;
    }
}
