using UnityEngine;

public class Player : Entity
{
    [field: SerializeField] public PlayerInput PlayerInputSO { get; private set; }
    private StateMarchine _stateMarchine;

    protected override void Awake()
    {
        base.Awake();
        _stateMarchine = new StateMarchine(this);
    }
}
