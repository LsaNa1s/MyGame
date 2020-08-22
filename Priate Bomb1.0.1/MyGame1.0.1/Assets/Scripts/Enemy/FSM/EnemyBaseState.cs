

public abstract class EnemyBaseState //抽象类状态机
{
    public abstract void EnterState(Enemy enemy);

    public abstract void OnUpdate(Enemy enemy);
}
