using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class EnemyHpSlider : MonoBehaviour,IEntityComponent,IAfterInit
{
    private EntityHealth _entityHealth;
    private float _maxHp;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _backSlider;
    private CanvasGroup canvasGroup;
    private EntityRenderer _renderer;

    [SerializeField] private RectTransform hpSlider;

    public void Initialize(Entity entity)
    {
        _entityHealth = entity.GetCompo<EntityHealth>();
        _entityHealth.hp.OnValueChanged += ChangeHp;
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        if(_hpSlider == null)
            _hpSlider = GetComponentInChildren<Slider>();
        _renderer = entity.GetCompo<EntityRenderer>();
        _renderer.OnFlip += Flip;
    }
    private void OnDestroy()
    {
        _renderer.OnFlip -= Flip;
    }
    private void Flip(bool LeftOrRight)
    {
        if (LeftOrRight)
        {
            hpSlider.transform.Rotate(0, 180f, 0);
        }
        else
        {
            hpSlider.transform.Rotate(0, 0, 0);
        }
    }

    public void AfterInitialize()
    {
        _maxHp = _entityHealth.maxHealth;
        _hpSlider.maxValue = _maxHp;
        _hpSlider.value = _maxHp;
        if (_backSlider != null)
        {
            _backSlider.maxValue = _maxHp;
            _backSlider.value = _maxHp;
        }
    }

    public void ChangeHp(float prev,float next)
    {
        if (_entityHealth.hp.Value <= 0f) DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 0.2f);

        print($"{prev}=>{next}");
        _hpSlider.value = _entityHealth._currentHealth;
        
        if (_backSlider != null && _backSlider.value > _hpSlider.value)
        {
            DOTween.Sequence()
                .AppendInterval(1f)
                .Append(_backSlider.DOValue(_entityHealth._currentHealth, 0.5f).SetEase(Ease.OutCubic));
        }
        else if (_backSlider != null && _backSlider.value < _hpSlider.value)
        {
            _backSlider.value = _entityHealth._currentHealth;
        }
    }

}
