using UnityEngine;

public class TestBoxEnemy : Entity
{
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
