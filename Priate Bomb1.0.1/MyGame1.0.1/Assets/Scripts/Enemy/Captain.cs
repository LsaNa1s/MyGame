using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Enemy, IDamageable
{
    SpriteRenderer sprite;

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

    public override void Init()
    {
        base.Init();
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();

        if(animState == 0)
        {
            sprite.flipX = false;
        }
    }
    public override void SkillAction()
    {
        base.SkillAction();
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("skill")){//判断是否播放动画skill
            sprite.flipX = true;
            if(transform.position.x > targetPoint.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, speed * 3 * Time.deltaTime);//移动到目标的右方向 
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, speed * 3 * Time.deltaTime);
            }
        }
        else
        {
            sprite.flipX = false;
        }




    }


}
