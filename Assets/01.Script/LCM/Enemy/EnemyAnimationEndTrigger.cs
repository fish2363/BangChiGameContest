using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEndTrigger : MonoBehaviour
{
    public UnityEvent OnDeadAnimationEnd;
    public UnityEvent OnAttackAnimationEnd;

    public void InvokeOnDeadAnimationEnd()
    {
        OnDeadAnimationEnd?.Invoke();
    }

    public void InvokeOnAttackAnimationEnd()
    {
        OnAttackAnimationEnd?.Invoke();
    }
}
