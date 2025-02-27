using UnityEngine;

public class QueenSlime_AttackState : EnemyState
{
    private float mass;
    public QueenSlime_AttackState(Enemy enemy) : base(enemy, EnemyStateType.Attack.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        AudioManager.Instance.PlaySound2D("QueenSlimeAttack",0,false,SoundType.SfX);
        mass = _enemy.RbCompo.mass;
        _enemy.RbCompo.mass = 100;
        _enemy.RandomAttack();
        _enemy.EntityHealth.IsInvincibility = true;
        _enemy.RbCompo.linearVelocity = Vector2.zero;
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        if(_enemy.isAttackAnimationEnd)
            _enemy.TransitionState(EnemyStateType.Move);
    }

    protected override void ExtiState()
    {
        _enemy.Attack();
        _enemy.isAttackAnimationEnd = false;
        _enemy.RbCompo.mass = mass;
        _enemy.EntityHealth.IsInvincibility = false;
        base.ExtiState();
    }
}
