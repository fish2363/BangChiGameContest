using System;
using System.Collections;
using UnityEngine;

public class LargeSlime : Enemy
{
    [SerializeField] private PoolItemSO _verticalBullet;
    [SerializeField] private PoolItemSO _horizontalBullet;
    [SerializeField] private Transform _firePos;
    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"LargeSlime_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignore
            }
        }
        TransitionState(EnemyStateType.Idle);
        AnimTriggerCompo.OnAttackTrigger += VerticalBulletFire;
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
        
    }
    private void VerticalBulletFire()
    {
        var bullet = PoolManager.Instance.Pop(_verticalBullet.poolName) as LargeSlimeBullet;
        bullet.transform.position = transform.position;
        bullet.ThrowObject(TargetTrm.position);
    }

    public override void Attakc2()
    {
        StartCoroutine(Attack2Coroutine());
    }

    private IEnumerator Attack2Coroutine()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var bullet = PoolManager.Instance.Pop(_horizontalBullet.poolName) as BossBullet;
            bullet.transform.position = _firePos.position;
            bullet.Initialize(transform.localScale.x >= 0f ? Vector2.right : Vector2.left);
        }
    }

    public override void RandomAttack()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        if(rand == 0)
            TransitionState(EnemyStateType.Attack);
        else
            TransitionState(EnemyStateType.Attack2);
    }

    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
    }
}
