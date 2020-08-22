using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Guy : Enemy, IDamageable
{

    public Transform pickupPoint;
    public float power;
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

    public void PickUpBomb()
    {
        if (targetPoint.CompareTag("Bomb") && !hasBomb)//如果目标点是炸弹标签,且手里没有炸弹
        {
            targetPoint.gameObject.transform.position = pickupPoint.position;//将举起坐标点赋值给炸弹

            targetPoint.SetParent(pickupPoint);//成为举起坐标点的子集

            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;//更改其刚体的属性

            hasBomb = true;
        
        }
    }

    public void ThrowAway()
    {
        if (hasBomb)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            targetPoint.SetParent(transform.parent.parent);//返回其祖父级2333

            if(FindObjectOfType<PlayerController>().gameObject.transform.position.x - transform.position.x < 0)//找到玩家坐标并与当前坐标对比
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * power, ForceMode2D.Impulse);//基于当前目标一个向左的力
            }
            else
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * power, ForceMode2D.Impulse);//反之
            }

        }
        hasBomb = false;
    }
}
