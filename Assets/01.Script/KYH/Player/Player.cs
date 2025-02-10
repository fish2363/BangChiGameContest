using UnityEngine;

public class Player : Entity
{
    [field: SerializeField] public GameEventChannelSO PlayerChannel { get; private set; }
    [field: SerializeField] public InputReader PlayerInput { get; private set; }
    private StateMachine _stateMachine;

    [field: SerializeField]
    public AnimParamSO ComboCounterParam { get; private set; }

    [SerializeField]
    private StateListSO playerFSM;

    public bool isBannedAttack { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine(this,playerFSM);
    }
    private void Start()
    {
        _stateMachine.ChangeState("IDLE");
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }
    private void HandleAnimationEnd()
    {
        _stateMachine.CurrentState.AnimationEndTrigger();
    }
    public void ChangeState(string newState) => _stateMachine.ChangeState(newState);


    private void OnDestroy()
    {
        GetCompo<EntityAnimationTrigger>().OnAnimationEnd -= HandleAnimationEnd;
        PlayerInput.ClearSubscription();
    }

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        GetCompo<EntityAnimationTrigger>().OnAnimationEnd += HandleAnimationEnd;
    }

    protected override void HandleHit()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleDead()
    {
        throw new System.NotImplementedException();
    }
}
