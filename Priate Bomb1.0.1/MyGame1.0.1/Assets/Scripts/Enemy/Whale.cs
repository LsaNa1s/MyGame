using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : Enemy, IDamageable
{
    public float scale;//放大的大小
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

    public void Swalow()
    {
        targetPoint.GetComponent<Bomb>().TurnOff();//调用熄灭炸弹方法
        targetPoint.gameObject.SetActive(false);//隐藏

        transform.localScale *= scale;//放大
    }
}
