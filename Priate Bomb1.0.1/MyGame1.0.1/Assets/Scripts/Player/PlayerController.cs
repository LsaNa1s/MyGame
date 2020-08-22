using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamageable
{

    private Rigidbody2D rb; //物理引擎
    private Animator anim;
    private FixedJoystick joystick;

    public float speed;
    public float jumpForce;

    public bool isMenu;

    [Header("Player State")]
    public float health;
    public bool isDead;


    [Header("Ground Check")]
    public Transform groundCheck;//获取transform组件的position 在unity中拖动transform
    public float checkRadius;//检测范围
    public LayerMask groundLayer;//对应图层Layer 在unity中选择ground图层

    [Header("States Check")]//方便区分管理，此为状态监测
    public bool isGround;
    public bool isJump;
    public bool canJump;

    [Header("Jump FX")]
    public GameObject jumpFX;
    public GameObject landFX;

    [Header("Attack Settings")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();

        GameManager.instance.IsPlayer(this);//将当前玩家类传过去

        health = GameManager.instance.LoadHealth();//获取存储的生命值
        UIManager.instanse.UpdateHealth(health);//更新血量
    }

    // Update is called once per frame
    void Update() //每一帧执行一次，获取input
    {
        anim.SetBool("dead", isDead);
        if (isDead)
        {
            return;//如果玩家死亡 则不执行下列任何命令直接返回
        }
        CheckInput();
        
    }

    public void FixedUpdate() //固定时间执行，差不多一秒执行一次，获取物理的方法。
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;//死亡时横纵坐标速度归零
            return;
        }
        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()//输入检测
    {
        if (Input.GetButtonDown("Jump") && isGround &&!isMenu)//是否输入跳跃属性，同时是否在地面上
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.J) && !isMenu)//是否输入j
        {
            Attack();
        }
    }

    void Movement()
    {
        
        //float horizontalInput = Input.GetAxis("Horizontal"); //键盘输入获取，在project setting里可以查看。-1 ~ 1 包括小数
        if (!isMenu)
        {
            //键盘操作
            /*float horizontalInput = Input.GetAxisRaw("Horizontal");//不包含小数
            if (horizontalInput != 0)
            {
                transform.localScale = new Vector3(horizontalInput, 1, 1);//人物翻转
            }*/

            //操纵杆
            float horizontalInput = joystick.Horizontal;
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);//2d的向量值


            if (horizontalInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if(horizontalInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }


        }
    }

    void Jump()
    {
        if (canJump)
        {
            isJump = true;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);//跳跃特效坐标跟随玩家腿部坐标
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//跳跃x轴随人物，y轴增加预设值
            rb.gravityScale = 4;//跳跃完成重力修改为4
            canJump = false;
        }
    }

    public void ButtonJump()
    {
        if(isGround)
            canJump = true;
    }

    public void Attack()
    {
        if(Time.time > nextAttack)
        {
            Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);//添加物体（炸弹预制体，玩家坐标，炸弹角度)

            nextAttack = Time.time + attackRate;//类似cd的效果
        }
    }

    void PhysicsCheck()//通过物理检测来判断是否在地面上
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position,checkRadius,groundLayer); //圆形检测(一个检测点(此处为坐标值)，一定的范围，对应的图层)
        if (isGround)
        {
            rb.gravityScale = 3;//落地之后重力再改为1
            isJump = false;
        }
    }

    public void LandFX()//animation引用特效
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }


    public void OnDrawGizmos()//区域检测显示
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);//在目标区域显示圆形的检测范围
    }

    public void GetHit(float damage)
    {
        if (!anim.GetCurrentAnimatorStateInfo(1).IsName("player_hit"))//第二个动画图层正在播放的动画名字
        {
            health = health - damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            anim.SetTrigger("hit");

            UIManager.instanse.UpdateHealth(health);//根据生命值显示UI心的数量
        }
    }
}
