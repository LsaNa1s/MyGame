using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy,IDamageable
{
    public Rigidbody2D rb;

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

/*    public override void Init()
    {
        base.Init();//在原有的函数基础执行一遍
        rb = GetComponent<Rigidbody2D>();
    }*/

    public void SetOff()//Animation Event 吹灭炸弹
    {
        targetPoint.GetComponent<Bomb>().TurnOff();//获得目标点的炸弹组件
    }
}
