using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;//定义一个当前状态类型 抽象类

    public Animator anim;
    public int animState;

    private GameObject alarmSign;//获得sign变量

    [Header("Base State")]
    public float health;
    public bool isDead;
    public bool hasBomb;
    public bool isBoss;

    [Header("Movement")]
    public float speed;
    public Transform pointA, pointB;
    public Transform targetPoint;//目标点
    public Transform hitPoint;

    [Header("Attack Setting")]
    public float nextAttack = 0;
    public float attackRate,skillRate;//攻击间隔
    public float attackRange, skillRange;//普攻距离，技能距离

    public List<Transform> attackList = new List<Transform>();

    public PatrolState patrolstate = new PatrolState();//定义一个方法实现抽象类继承子集中的方法，当前为巡逻状态
    public AttackState attackstate = new AttackState();
    
    public virtual void Init()
    {
        anim = GetComponent<Animator>();
        alarmSign = transform.GetChild(0).gameObject;

        
    }

    public void Awake()//awake会优先于start调用
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.IsEnemy(this);
        TransitionToState(patrolstate);//开始时状态机为巡逻状态
        if (isBoss)
            UIManager.instanse.SetBossHealth(health);//如果是boss 将血量传给ui的血量条
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
        {
            GameManager.instance.EnemyDead(this);
            return;
            
        }

        currentState.OnUpdate(this);//当前敌人执行相应状态机
        anim.SetInteger("state", animState);//每一帧都上传动画控制器中的数值

        if (isBoss)
            UIManager.instanse.UpdateBossHealth(health);
    }

    public void TransitionToState(EnemyBaseState state)//状态机切换
    {
        currentState = state;//进入状态方法
        currentState.EnterState(this);
    }

    public void MoveToTarget()//移动
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);//移动到（本身位置，目标位置，速度*机身时间)

        FilpDirection();
    }

    public void AttackAction()//普通攻击
    {
        
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)//x为距离，y为半径 画圆来充当范围,人物与目标的距离小于攻击范围
        {
            if(Time.time > nextAttack)//cd时间
            {
                //播放攻击动画
                anim.SetTrigger("attack");
                Debug.Log("普通平A");
                nextAttack = Time.time + attackRate;
            }
        }
    }

    public virtual void SkillAction()//技能攻击,虚方法，子类可重写
    {
        
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)//x为距离，y为半径 画圆来充当范围,人物与目标的距离小于攻击范围
        {
            if (Time.time > nextAttack)//cd时间
            {
                //播放攻击动画
                anim.SetTrigger("skill");
                Debug.Log("技能攻击");
                nextAttack = Time.time + skillRate;
            }
        }
    }

    public void FilpDirection()//翻转人物
    {
        if (transform.position.x < targetPoint.position.x)//目标在人物右侧
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void SwitchPoint()//目标点设置
    {
        if(Mathf.Abs(pointA.position.x - transform.position.x) > Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)//碰撞体包含检测
    {
        if(!attackList.Contains(collision.transform) && !hasBomb && !isDead && !GameManager.instance.gameOver)
            attackList.Add(collision.transform);//添加被检测到的碰撞体
    }

    private void OnTriggerExit2D(Collider2D collision)//碰撞体移除检测
    {
        attackList.Remove(collision.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)//碰撞体进入检测
    {
        if(!isDead && !GameManager.instance.gameOver)
            StartCoroutine(OnAlarm());//开始目标携程

    }

    IEnumerator OnAlarm()//多线程携程 显示叹号的
    {
        //上一帧执行完等待下一帧 在执行
        alarmSign.SetActive(true);//显示
        yield return new WaitForSeconds(alarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);//获得第一个默认layer的第一个默认动画的片段的长度，返回 等待这个长度的时间
        alarmSign.SetActive(false);//隐藏
    }

}
