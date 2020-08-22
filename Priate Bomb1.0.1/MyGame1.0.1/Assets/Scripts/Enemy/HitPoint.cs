using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public bool bombAvilable;//是否可以踢飞炸弹
    public int bombFlyPower;//击飞炸弹的力
    int dir;//方向

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > other.transform.position.x) //人物在炸弹的右侧,获取攻击方向
            dir = -1;
        else
            dir = 1;

        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家受伤");
            other.GetComponent<IDamageable>().GetHit(1);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * bombFlyPower, ForceMode2D.Impulse);//给予其刚体一个向某个方向的力
        }

        if (other.CompareTag("Bomb") && bombAvilable)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * bombFlyPower, ForceMode2D.Impulse);//给予其刚体一个向某个方向的力
        }
    }
}
