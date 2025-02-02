using UnityEngine;

public abstract class EnemyState
{
    protected Enemy _enemy;
    protected int _animBoolHash;

    public EnemyState(Enemy enemy, string animBoolHash){
        _enemy = enemy;
        _animBoolHash = Animator.StringToHash(animBoolHash);
    }


    public void Enter(){

        EnterState();
    }

    protected virtual void EnterState()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, true);
    }

    public void Exit()
    {

        ExtiState();
    }

    protected virtual void ExtiState()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, false);
    }

    public virtual void UpdateState()
    {

    }

    public virtual void FixedUpdateState()
    {

    }
}
