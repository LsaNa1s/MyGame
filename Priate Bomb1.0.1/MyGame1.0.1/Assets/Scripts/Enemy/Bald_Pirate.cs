using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bald_Pirate : Enemy, IDamageable
{
    public GameObject DD;
    public GameObject DDpoint;

    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }

    

}
