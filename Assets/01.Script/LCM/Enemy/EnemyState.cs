using UnityEngine;

public class EnemyState
{
    protected Enemy _enemy;

    public EnemyState(Enemy enemy){
        _enemy = enemy;
    }


    public void Enter(){

        EnterState();
    }

    protected virtual void EnterState()
    {

    }

    public void Exit()
    {

        ExtiState();
    }

    protected virtual void ExtiState()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void FixedUpdateState()
    {

    }
}
