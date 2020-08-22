using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    private Rigidbody2D rb;

    public float startTime;//爆炸开始的时间
    public float waitTime;//爆炸等待的时间
    public float bombForce;//爆炸产生的力

    [Header("Check")]
    public float radius;//爆炸影响的范围
    public LayerMask targetLayer;//影响 图层

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;//获取爆炸开始的时间并赋内置的时间

    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))//如果开始播放的是不熄灭动画，则计时爆炸
        if (Time.time > startTime + waitTime)
        {
            anim.Play("bomb_explotion");//播放爆炸动画
        }
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);//检测范围圆形显示（该图片位置，影响范围）
    }

    public void Explotion()//animation event
    {
        coll.enabled = false;//在爆炸开始的时候取消自身碰撞体

        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);//检测方法的所有物体返回数组（检测位置，范围，目标图层）

        rb.gravityScale = 0;//因为碰撞体取消会因重力掉落 所以将重力改为0

        foreach (var item in aroundObjects)
        {
            Vector3 pos = transform.position - item.transform.position;//本体与被碰撞物体们的坐标方向值

            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * bombForce, ForceMode2D.Impulse);//获取组件后添加力（(力的相反方向+向上的力)*威力，冲击力）
            if (item.CompareTag("Bomb") && item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))//判断碰撞体标签是炸弹并且正处于播放熄灭动画
            {
                item.GetComponent<Bomb>().TurnOn();//再次启动炸弹

            }
            if (item.CompareTag("Player"))
            {
                item.GetComponent<IDamageable>().GetHit(3);
            }
        }
    }

    public void DestroyThis()//animation event
    {
        Destroy(gameObject);
    }

    public void TurnOff()
    {
        anim.Play("bomb_off");//播放熄灭动画
        gameObject.layer = LayerMask.NameToLayer("NPC");//更高图层为npc来规避检测
    }

    public void TurnOn()
    {
        startTime = Time.time;//启动时间再次归零
        anim.Play("bomb_on");//播放点火动画
        gameObject.layer = LayerMask.NameToLayer("Bomb");//回到图层并再次被检测
    }
}
