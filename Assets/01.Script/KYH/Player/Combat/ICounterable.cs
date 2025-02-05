using UnityEngine;

public interface ICounterable
{
    public bool CanCounter { get; }

    public Transform TargetTrm { get; }

    //��������� float�� DamageData��� ����ü�� �ѱ沨��.
    public void ApplyCounter(float damage, Vector2 direction, Vector2 knockBackForce,
                                bool isPowerAttack, Entity dealer);
}
