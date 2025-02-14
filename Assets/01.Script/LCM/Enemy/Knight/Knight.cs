using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knight : Enemy
{
    private Stack<int> _attackPattern = new Stack<int>();
    
    [SerializeField] private float _jumpPower;
    
    private bool _isPageTwo;

    [SerializeField] private float _takeShieldCoolTime;
    [SerializeField] private float _ShieldCoolTime;
    private float _lastAbilityTime;

    public UnityEvent OnPrepareShield;
    public UnityEvent OnDestroyShield;
    
    [SerializeField] private ParticleSystem _shieldParticle;
    [SerializeField] private ParticleSystem _attack2Particle;
    [SerializeField] private ParticleSystem _attack3Particle;
    [SerializeField] private ParticleSystem _attack4EnergyParticle;
    [SerializeField] private ParticleSystem _attack4Particle;
    [SerializeField] private ParticleSystem _attack5Particle;
    
    
    [SerializeField] private ParticleSystem _pageTwoParticle;
    [SerializeField] private ParticleSystem _pageTwoExplosionParticle;
    [SerializeField] private ParticleSystem _auraParticle;

    [SerializeField] private int _shieldAttackCount;

    private float _nowTime;
    private int _hitCount;
    private bool _isTakeShield = false;
    private bool _isAlreadyExplosion = false;

    private EnemyAttackCompo _enemyAttackCompo;
    
    public List<EnemyAttackStruct> _knightAttacks;

    private int _attackIndex;

    [SerializeField] private PoolItemSO _spinSword;
    [SerializeField] private float _spinSwordSpawnTime;
    private float _curTime = 100000f;
    
    
    [SerializeField] private PoolItemSO _bossBullet;
    protected override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"Knight_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch
            {
                // ignore
            }
        }

        _enemyAttackCompo = GetComponentInChildren<EnemyAttackCompo>();
        
        EntityHealth.hp.OnValueChanged += HandleHpDown;
        TransitionState(EnemyStateType.Idle);
        
        _attackPattern.Push(1);
    }
    

    private void HandleHpDown(float prev, float next)
    {
        if (_isTakeShield)
        {
            _hitCount++;
        }
        if (EntityHealth.maxHealth / 2 >= next && !_isPageTwo)
        {
            Debug.Log("2페이즈 진입");
            _isPageTwo = true;
            TransitionState(EnemyStateType.PageTwo);
            AllEffectEnd();
            StartCoroutine(PageTwoCoroutine());
        }
    }

    private void AllEffectEnd()
    {
        StopAllCoroutines();
        _hitCount = 100;
        _shieldParticle.Stop();
        _attack2Particle.Stop();
        _attack3Particle.Stop();
        _auraParticle.Stop();
        _attack4Particle.Stop();
        _attack4EnergyParticle.Stop();
    }

    private IEnumerator PageTwoCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _pageTwoParticle.Play();
        yield return new WaitForSeconds(3f);
        _pageTwoExplosionParticle.Play();
        yield return new WaitForSeconds(2f);
        _hitCount = 0;
        TransitionState(EnemyStateType.Idle);
        CreateShield();
        _lastAbilityTime = Time.time;
        _curTime = Time.time;
        _auraParticle.Play();
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
        _attackIndex = 0;
        _enemyAttackCompo.AttackSetting(_knightAttacks[_attackIndex].damage, _knightAttacks[_attackIndex].force,
            _knightAttacks[_attackIndex].attackBoxSize, _knightAttacks[_attackIndex].attackRadius, _knightAttacks[_attackIndex].castType);
    }

    public override void Attakc2()
    {
        _attackIndex = 1;
        _enemyAttackCompo.AttackSetting(_knightAttacks[_attackIndex].damage, _knightAttacks[_attackIndex].force,
            _knightAttacks[_attackIndex].attackBoxSize, _knightAttacks[_attackIndex].attackRadius, _knightAttacks[_attackIndex].castType);
        RbCompo.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        RbCompo.AddForce(transform.right * 2f, ForceMode2D.Impulse);
        StartCoroutine(Attack2Coroutine());
    }
    public override void Attakc3()
    {
        _attackIndex = 2;
        _enemyAttackCompo.AttackSetting(_knightAttacks[_attackIndex].damage, _knightAttacks[_attackIndex].force,
            _knightAttacks[_attackIndex].attackBoxSize, _knightAttacks[_attackIndex].attackRadius, _knightAttacks[_attackIndex].castType);
        StartCoroutine(Attack3Coroutine());
    }

    public override void Attakc4()
    {
        _attackIndex = 3;
        _enemyAttackCompo.AttackSetting(_knightAttacks[_attackIndex].damage, _knightAttacks[_attackIndex].force,
            _knightAttacks[_attackIndex].attackBoxSize, _knightAttacks[_attackIndex].attackRadius, _knightAttacks[_attackIndex].castType);
        StartCoroutine(Attack4Coroutine());
    }

    public override void Attakc5()
    {
        _attackIndex = 4;
        _enemyAttackCompo.AttackSetting(_knightAttacks[_attackIndex].damage, _knightAttacks[_attackIndex].force,
            _knightAttacks[_attackIndex].attackBoxSize, _knightAttacks[_attackIndex].attackRadius, _knightAttacks[_attackIndex].castType);
        StartCoroutine(Attack5Coroutine());
    }



    private IEnumerator Attack2Coroutine()
    {
        yield return new WaitForSeconds(0.65f);
        _attack2Particle.Play();
    }
    private IEnumerator Attack3Coroutine()
    {
        yield return new WaitForSeconds(0.6f);
        _attack3Particle.transform.Rotate(180,0,0);
        _attack3Particle.Play();
        yield return new WaitForSeconds(0.8f);
        _attack3Particle.transform.Rotate(180,0,0);
        _attack3Particle.Play();
    }
    private IEnumerator Attack4Coroutine()
    {
        _attack4EnergyParticle.Play();
        yield return new WaitForSeconds(1.6f);
        _attack4Particle.Play();
    }
    private IEnumerator Attack5Coroutine()
    {
        _attack5Particle.Play();
        yield return new WaitForSeconds(0.6f);
        var bullet1 = PoolManager.Instance.Pop(_bossBullet.poolName) as MonoBehaviour;
        bullet1.transform.position = transform.position;
        yield return new WaitForSeconds(0.4f);
        var bullet2 = PoolManager.Instance.Pop(_bossBullet.poolName) as MonoBehaviour;
        bullet2.transform.position = transform.position;
    }

    public override void RandomAttack()
    {
        int rand = 0;
        if (!_isPageTwo)
        {
            while (_attackPattern.Peek() == rand)
            {
                rand = UnityEngine.Random.Range(0, 2);
            }
            
            _attackPattern.Push(rand);
            
            if(rand == 0)
                TransitionState(EnemyStateType.Attack);
            else
                TransitionState(EnemyStateType.Attack2);
        }
        else
        {
            while (_attackPattern.Peek() == rand)
            {
                rand = UnityEngine.Random.Range(0, 4);
            }
            
            _attackPattern.Push(rand);
            if(rand == 0)
                TransitionState(EnemyStateType.Attack);
            else if(rand == 1)
                TransitionState(EnemyStateType.Attack2);
            else if (rand == 2)
                TransitionState(EnemyStateType.Attack3);
            else
                TransitionState(EnemyStateType.Attack4);          
        }
    }

    private void SpawnSpinSword()
    {
        if (_isPageTwo)
        {
            var spinsword = PoolManager.Instance.Pop(_spinSword.poolName) as MonoBehaviour;  
            spinsword.transform.position = new Vector3(TargetTrm.position.x, 0,0);
            _curTime = Time.time;
        }
    }

    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
        AllEffectEnd();
    }

    public override void IsCanShield()
    {
        if (Time.time >= _lastAbilityTime + _takeShieldCoolTime)
        {
            TakeShield();
            _lastAbilityTime = Time.time;
        }
    }

    private void TakeShield()
    {
        TransitionState(EnemyStateType.Shield);
        OnPrepareShield?.Invoke();
    }

    public override void CreateShield()
    {
        _shieldParticle.Play();
        _nowTime = Time.time;
        _isTakeShield = true;
        _isAlreadyExplosion = false;
        EntityHealth.IsShield = true;
    }

    private void ShieldCooldown()
    {
        if (Time.time >= _ShieldCoolTime + _nowTime || _hitCount >= _shieldAttackCount)
        {
            OnDestroyShield?.Invoke();
            _shieldParticle.Stop();
            _isTakeShield = false;
            _isAlreadyExplosion = true;
            _hitCount = 0;
            EntityHealth.IsShield = false;
        }
    }

    protected override void Update()
    {
        base.Update();
        if(!_isTakeShield || _isAlreadyExplosion) return;
        
        ShieldCooldown();
        
        if(Time.time >= _curTime + _spinSwordSpawnTime)
            SpawnSpinSword();
    }
}
