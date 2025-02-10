using System;
using UnityEngine;

public class TestBoxEnemy : Entity
{
    [SerializeField] private Rigidbody2D RbCompo;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        GetCompo<EntityHealth>().OnKnockback += HandleKnockBack;
    }

    private void HandleKnockBack(Vector2 knockBackForce)
    {
        float knockBackTime = 0.5f;
        KnockBack(knockBackForce, knockBackTime);
    }

    public void AddForceToEntity(Vector2 force)
            => RbCompo.AddForce(force, ForceMode2D.Impulse);
    public void StopImmediately(bool isYAxisToo)
    {
        if (isYAxisToo)
            RbCompo.linearVelocity = Vector2.zero;
        else
            RbCompo.linearVelocityX = 0;
    }

    private void KnockBack(Vector2 knockBackForce, float knockBackTime)
    {
        StopImmediately(true);
        AddForceToEntity(knockBackForce);
    }

    protected override void HandleDead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        print("데스스테이트");
    }

    protected override void HandleHit()
    {
        print("아야");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
