using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHpSlider : MonoBehaviour,IEntityComponent,IAfterInit
{
    private EntityHealth _entityHealth;
    private float _maxHp;
    private Slider _slider;
    private CanvasGroup canvasGroup;

    public void Initialize(Entity entity)
    {
        _entityHealth = entity.GetCompo<EntityHealth>();
        _slider = GetComponentInChildren<Slider>();
        _entityHealth.hp.OnValueChanged += ChangeHp;
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }
    public void AfterInitialize()
    {
        _maxHp = _entityHealth.maxHealth;
        _slider.maxValue = _maxHp;
        _slider.value = _maxHp;
    }

    public void ChangeHp(float prev,float next)
    {
        if (_entityHealth._currentHealth <= 1) DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 0.2f);

        print($"{prev}=>{next}");
        _slider.value = _entityHealth._currentHealth;
    }

}
