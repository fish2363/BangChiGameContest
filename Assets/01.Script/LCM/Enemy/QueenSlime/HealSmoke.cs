using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HealSmoke : MonoBehaviour
{
    [SerializeField] private Vector2 _checkBoxSize;
    [SerializeField] private LayerMask _whatIsEnemy;
    
    [SerializeField] private float _healAmount;
    [SerializeField] private float _waitTime;
    private void OnEnable()
    {
        StartCoroutine(HealCoroutine());
    }

    private IEnumerator HealCoroutine()
    {
        var colliders = Physics2D.OverlapBoxAll(transform.position, _checkBoxSize,0, _whatIsEnemy);
        foreach (var enemy in colliders)
        {
            var health = enemy.GetComponentInChildren<EntityHealth>();
            health.TakeHeal(_healAmount);
        }

        yield return new WaitForSeconds(_waitTime);
        StartCoroutine(HealCoroutine());
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _checkBoxSize);
        Gizmos.color = Color.white;
    }
#endif
}
