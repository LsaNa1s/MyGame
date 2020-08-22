using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//获取动画组件控制器
        rb = GetComponent<Rigidbody2D>();//获取物理刚体控制器
        controller = GetComponent<PlayerController>();//获取玩家代码
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));//字符串名字与x轴移动速度，速度会小于0，所以用绝对值
        anim.SetFloat("velocityY", rb.velocity.y);//因为需要下落速度小于零所以不需要绝对值
        anim.SetBool("jump", controller.isJump);
        anim.SetBool("ground", controller.isGround);
    
    }
}
