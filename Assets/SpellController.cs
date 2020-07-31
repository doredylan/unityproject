using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public Transform player;
    public GameObject lighteningTomeSlot;
    public GameObject fireTomeSlot;
    public bool lspell = false;
    public bool fspell = false;
 

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((player.position.x - 5), (player.position.y -5), transform.position.z);

        spellTomeCollisionTracker();
       
    }


    void spellTomeCollisionTracker()
    {

        if (lspell == true)
        {
            lighteningTomeSlot.SetActive(true);
          
        }
        if (fspell == true )
        {
            fireTomeSlot.SetActive(true);
        } 
    }
}
