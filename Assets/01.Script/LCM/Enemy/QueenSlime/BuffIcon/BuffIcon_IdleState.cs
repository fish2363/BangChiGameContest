using UnityEngine;

public class BuffIcon_IdleState : EnemyState
{
    public BuffIcon_IdleState(Enemy enemy) : base(enemy, EnemyStateType.Idle.ToString())
    {
    }
}
