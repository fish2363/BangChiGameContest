using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Knight : Enemy
{
    [SerializeField] private float _jumpPower;
    
    [SerializeField] private EntityHealth _entityHealth;
    private bool _isPageTwo;

    [SerializeField] private float _takeShieldCoolTime;
    [SerializeField] private float _ShieldCoolTime;
    private float _lastAbilityTime;

    public UnityEvent OnPrepareShield;
    public UnityEvent OnDestroyShield;
    
    [SerializeField] private ParticleSystem _shieldParticle;
    [SerializeField] private ParticleSystem _attackParticle;

    [SerializeField] private int _shieldAttackCount;

    private float _nowTime;
    private int _hitCount;
    private bool _isTakeShield = false;
    private bool _isAlreadyExplosion = false;
    

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

        _entityHealth.hp.OnValueChanged += HandleHpDown;
        TransitionState(EnemyStateType.Idle);
    }
    

    private void HandleHpDown(float prev, float next)
    {
        if (_isTakeShield)
        {
            _hitCount++;
        }
        if (_entityHealth.maxHealth / 2 <= next)
        {
            Debug.Log("2페이즈 진입");
            _isPageTwo = true;
        }
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
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        _attackParticle.Play();
        yield return new WaitForSeconds(0.8f);
        _attackParticle.Play();
    }

    public override void Attakc2()
    {
        RbCompo.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        RbCompo.AddForce(transform.right * 2f, ForceMode2D.Impulse);
    }

    public override void RandomAttack()
    {
        if (!_isPageTwo)
        {
            int rand = UnityEngine.Random.Range(0, 2);
            if(rand == 0)
                TransitionState(EnemyStateType.Attack);
            else
                TransitionState(EnemyStateType.Attack2);
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, 3);
            if(rand == 0)
                TransitionState(EnemyStateType.Attack);
            else if(rand == 1)
                TransitionState(EnemyStateType.Attack2);
            else
                TransitionState(EnemyStateType.Attack3);
        }
    }
    public override void Dead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
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
    }

    protected override void Update()
    {
        base.Update();
        if(!_isTakeShield || _isAlreadyExplosion) return;
        
        if (Time.time >= _ShieldCoolTime + _nowTime || _hitCount >= _shieldAttackCount)
        {
            OnDestroyShield?.Invoke();
            _shieldParticle.Stop();
            _isTakeShield = false;
            _isAlreadyExplosion = true;
            _hitCount = 0;
        }
    }
}
