using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        Debug.Log("发现敌人！");
        enemy.animState = 2;
        enemy.targetPoint = enemy.attackList[0];//将目标点设定为第一个list
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (enemy.hasBomb)
            return;
        if(enemy.attackList.Count == 0)//检测无物体回到巡逻状态
        {
            enemy.TransitionToState(enemy.patrolstate);
        }
        if (enemy.attackList.Count > 1)//如果检测物品为两个以上
        {
            for (int i = 0; i < enemy.attackList.Count; i++)//遍历list判断最近物体
            {
                if (Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x) <
                    Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))
                {
                    enemy.targetPoint = enemy.attackList[i];
                }
            }
        }
        if(enemy.attackList.Count == 1)
            enemy.targetPoint = enemy.attackList[0];
        
        if (enemy.targetPoint.CompareTag("Player"))//目标点的标签为玩家
        {
            enemy.AttackAction();
        }
        if (enemy.targetPoint.CompareTag("Bomb"))//目标点标签为炸弹
        {
            enemy.SkillAction();
        }
        enemy.MoveToTarget();
    }
}
