using UnityEngine;

public abstract class EntityState
{
    protected Entity _entity;

    protected bool _isTriggerCall;

    protected EntityRenderer _renderer;

    public EntityState(Entity entity)
    {
        _entity = entity;
        _renderer = _entity.GetCompo<EntityRenderer>(true);
    }

    public virtual void Enter()
    {
        _isTriggerCall = false;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
    }

    public virtual void AnimationEndTrigger() => _isTriggerCall = true;
}
