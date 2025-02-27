using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSkeleton : Enemy
{
    public List<EnemyAttackStruct> _kingSkeletonAttacks;
    
    private int _attackIndex;
    private float _curTime;
    private EnemyAttackCompo _enemyAttackCompo;

    [SerializeField] private float _attack3CoolTime;

    [SerializeField] private ParticleSystem _buffPart;
    [SerializeField] private ParticleSystem _buffExplosionPart;
    [SerializeField] private float _buffTime;
    private float _attackDamageMultiple = 1f;
    
    
    [SerializeField] private GameObject _gladiator;
    [SerializeField] private GameObject _marksMan;

    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"KingSkeleton_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignore
            }
        }

        _enemyAttackCompo = GetComponentInChildren<EnemyAttackCompo>();
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
        
    }

    protected override void HandleHit()
    {
    }

    protected override void HandleDead() => Dead();

    public override void Attack()
    {
        _attackIndex = 0;
        _enemyAttackCompo.AttackSetting(_kingSkeletonAttacks[_attackIndex].damage * _attackDamageMultiple, _kingSkeletonAttacks[_attackIndex].force,
            _kingSkeletonAttacks[_attackIndex].attackBoxSize, _kingSkeletonAttacks[_attackIndex].attackRadius, _kingSkeletonAttacks[_attackIndex].castType);
        StartCoroutine(AttackAudioCoroutine());
    }

    private IEnumerator AttackAudioCoroutine()
    {
        yield return new WaitForSeconds(0.7f);
        AudioManager.Instance.PlaySound2D("KingSkeletonAttack",2f,false,SoundType.SfX);
    }

    public override void Attakc2()
    {
        _attackIndex = 1;
        _enemyAttackCompo.AttackSetting(_kingSkeletonAttacks[_attackIndex].damage * _attackDamageMultiple, _kingSkeletonAttacks[_attackIndex].force,
            _kingSkeletonAttacks[_attackIndex].attackBoxSize, _kingSkeletonAttacks[_attackIndex].attackRadius, _kingSkeletonAttacks[_attackIndex].castType);
        StartCoroutine(Attack2AudioCoroutine());
    }
    
    private IEnumerator Attack2AudioCoroutine()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlaySound2D("KingSkeletonAttack2",2f,false,SoundType.SfX);
    }

    public override void Attakc3()
    {
        StartCoroutine(BuffCoolTimeCoroutine());
        float rand = UnityEngine.Random.Range(- 5f, 5f);
        var gladiator = Instantiate(_gladiator, new Vector3(transform.position.x + rand, transform.position.y - 0.5f), Quaternion.identity);
        gladiator.GetComponent<Gladiator>().Spawn();
        var marksman = Instantiate(_marksMan, new Vector3(transform.position.x - rand, transform.position.y - 0.5f), Quaternion.identity);
        marksman.GetComponent<Marksman>().Spawn();
    }
    

    public override void RandomAttack()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == 0)
            TransitionState(EnemyStateType.Attack);
        else if (rand == 1)
            TransitionState(EnemyStateType.Attack2);
    }

    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
        AudioManager.Instance.PlaySound2D("KingSkeletonDead",0,false,SoundType.SfX);
    }

    protected override void Update()
    {
        base.Update();
        _curTime += Time.deltaTime;
        if (_curTime >= _attack3CoolTime &&
            (_currentState != EnemyStateType.Attack && _currentState != EnemyStateType.Attack2) && !IsDead)
        {
            TransitionState(EnemyStateType.Attack3);
            _curTime = 0;
        }
    }

    private IEnumerator BuffCoolTimeCoroutine()
    {
        _buffPart.Play();
        _attackDamageMultiple = 2f;
        yield return new WaitForSeconds(_buffTime);
        _buffPart.Stop();
        _buffExplosionPart.gameObject.SetActive(true);
        _attackDamageMultiple = 1f;
        yield return new WaitForSeconds(1f);
        _buffExplosionPart.gameObject.SetActive(false);
    }
}
