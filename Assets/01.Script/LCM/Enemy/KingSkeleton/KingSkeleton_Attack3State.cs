using UnityEngine;

public class KingSkeleton_Attack3State : EnemyState
{
    private float mass;
    public KingSkeleton_Attack3State(Enemy enemy) : base(enemy, EnemyStateType.Attack3.ToString())
    {
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        _enemy.RbCompo.linearVelocity = Vector2.zero;
        mass = _enemy.RbCompo.mass;
        _enemy.RbCompo.mass = 100;
        AudioManager.Instance.PlaySound2D("KingSkeletonAttack3",0,false,SoundType.SfX);
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        if(_enemy.isAttackAnimationEnd)
            _enemy.TransitionState(EnemyStateType.Move);
    }

    protected override void ExtiState()
    {
        base.ExtiState();
        _enemy.isAttackAnimationEnd = false;
        _enemy.RbCompo.mass = mass;
        _enemy.Attakc3();
    }
}
