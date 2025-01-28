using System;
using UnityEngine;

public class EntityAnimationTrigger : MonoBehaviour,IEntityComponent
{
    public event Action OnAnimationEnd;
    public event Action OnAttackTrigger;

    private Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private void AnimationEnd() => OnAnimationEnd?.Invoke();
    private void AttackTrigger() => OnAttackTrigger?.Invoke();

}
