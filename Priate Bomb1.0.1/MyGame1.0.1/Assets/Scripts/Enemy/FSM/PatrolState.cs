using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;
        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))//判断idle动画是否播放完毕
        {
            enemy.animState = 1;
            enemy.MoveToTarget();
        }

        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01f)//当物体坐标接近目标点 再次执行
        {
            enemy.TransitionToState(enemy.patrolstate);//再次切换为初始进入的状态
        }
        
        if(enemy.attackList.Count > 0)//list数量大于零
        {
            enemy.TransitionToState(enemy.attackstate);//切换为攻击状态
        }

    }
}
