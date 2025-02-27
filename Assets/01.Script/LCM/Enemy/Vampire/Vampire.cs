using System;
using System.Collections;
using UnityEngine;

public class Vampire : Enemy
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private PoolItemSO _bullet1;
    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"Vampire_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignore
            }
        }
        TransitionState(EnemyStateType.Idle);
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
        print("넉백");
        float knockBackTime = 0.5f;
        KnockBack(knockBackForce, knockBackTime);
    }
    protected override void HandleHit()
    {
        
    }

    protected override void HandleDead() => Dead();

    public override void Attack()
    {
        AudioManager.Instance.PlaySound2D("VampireAttack",0,false,SoundType.SfX);
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        var bullet = PoolManager.Instance.Pop(_bullet1.poolName) as BossBullet;
        bullet.transform.position = _firePos.position;
        bullet.Initialize(transform.localScale.x >= 0f ? Vector2.right : Vector2.left);
    }

    public override void Attakc2()
    {
        AudioManager.Instance.PlaySound2D("VampireAttack2",0,false,SoundType.SfX);
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
        AudioManager.Instance.PlaySound2D("VampireDead",0,false,SoundType.SfX);
    }
}
