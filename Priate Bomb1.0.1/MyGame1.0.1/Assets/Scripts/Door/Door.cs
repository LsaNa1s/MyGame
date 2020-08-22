using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    BoxCollider2D coll;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        GameManager.instance.IsExitDoor(this);

        coll.enabled = false;//碰撞体取消
    }

    public void OpenDoor()//在game manager 中调用
    {
        anim.Play("open");//播放动画
        coll.enabled = true;//碰撞体开启
    }

    private void OnTriggerEnter2D(Collider2D collision)//检测碰撞体进入
    {
        if (collision.CompareTag("Player"))
        {
            //去下一个房间
            GameManager.instance.NextLevel();
        }
    }

}
