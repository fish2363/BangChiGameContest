using System;
using UnityEngine;

public class EntityRenderer : MonoBehaviour, IEntityComponent
{
    public SpriteRenderer SpriteRenderer { get; private set; }

    [field: SerializeField] public float FacingDirection { get; private set; } = 1f;

    private Entity _entity;
    private Animator _animator;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetParam(string param, bool value) => _animator.SetBool(Animator.StringToHash(param),value);
    public void SetParam(string param, int value) => _animator.SetInteger(Animator.StringToHash(param), value);
    public void SetParam(string param, float value) => _animator.SetFloat(Animator.StringToHash(param), value);
    public void SetParam(string param) => _animator.SetTrigger(Animator.StringToHash(param));

    public void FlipController(float xVelocity)
    {
        float xMove = Mathf.Approximately(xVelocity, 0) ? 0 : Mathf.Sign(xVelocity);
        if(Mathf.Abs(xMove + FacingDirection) < 0.5f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        _entity.transform.Rotate(0,180f,0);
    }
}
