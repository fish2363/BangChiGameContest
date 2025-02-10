using System;
using UnityEngine;

public class TestBoxEnemy : Entity
{
    private void Awake()
    {
        GetCompo<EntityHealth>().OnKnockback += HandleKnockBack;
    }

    private void HandleKnockBack(Vector2 obj)
    {
        float knockBackTime = 0.5f;
        _mover.KnockBack(knockBackForce, knockBackTime);
    }

    protected override void HandleDead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        print("����������Ʈ");
    }

    protected override void HandleHit()
    {
        print("�ƾ�");
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
