using System;
using UnityEngine;

public class Marksman : Enemy
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private PoolItemSO _arrow;
    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"Marksman_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignored
            }
        }
        TransitionState(EnemyStateType.Idle);
        AnimTriggerCompo.OnAttackTrigger += Attack;
    }

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        GetCompo<EntityHealth>().OnKnockback += HandleKnockBack;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GetCompo<EntityHealth>().OnKnockback -= HandleKnockBack;
    }

    private void HandleKnockBack(Vector2 knockBackForce)
    {
        float knockBackTime = 0.5f;
        KnockBack(knockBackForce, knockBackTime);
    }

    protected override void HandleHit()
    {
        
    }

    protected override void HandleDead() => Dead();

    public override void Attack()
    {
        var arrow = PoolManager.Instance.Pop(_arrow.poolName) as MarksmanBullet;
        arrow.transform.position = _firePos.position;
        arrow.ThrowObject(TargetTrm.position);
        AudioManager.Instance.PlaySound2D("MarksmanAttack", 0,false,SoundType.SfX);
    }

    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
        AudioManager.Instance.PlaySound2D("MarksmanDead", 0,false,SoundType.SfX);
    }

    public void Spawn()
    {
        TransitionState(EnemyStateType.Spawn);
    }
}
