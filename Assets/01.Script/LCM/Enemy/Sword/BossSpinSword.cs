using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossSpinSword : Entity, IPoolable
{
    [SerializeField] private string _poolName;
    [SerializeField] private float _waitTime;
    [SerializeField] private ParticleSystem _spinEffect;
    public UnityEvent OnExplosion;
    public string PoolName => _poolName;
    public GameObject ObjectPrefab => gameObject;

    private void OnEnable()
    {
        StartCoroutine(SwordAttackCoroutine());
    }

    private IEnumerator SwordAttackCoroutine()
    {
        yield return new WaitForSeconds(_waitTime);
        AudioManager.Instance.PlaySound2D("BossSpinSword",0,false,SoundType.SfX);
        _spinEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(_waitTime);
        OnExplosion?.Invoke();
        yield return new WaitForSeconds(0.1f);
        PoolManager.Instance.Push(this);
    }

    public void ResetItem()
    {
        _spinEffect.gameObject.SetActive(false);
    }

    protected override void HandleHit()
    {
        
    }

    protected override void HandleDead()
    {
        
    }
}
