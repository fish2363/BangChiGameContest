using UnityEngine;

public class FallState : PlayerAirState
{
    public FallState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
    }
    public override void Update()
    {
        base.Update();
        if (_mover.IsGroundDetected())
        {
            _player.ChangeState("IDLE");
            Debug.Log("���� ����");
        }
    }
}
