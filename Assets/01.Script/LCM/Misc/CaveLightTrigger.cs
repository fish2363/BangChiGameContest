using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CaveLightTrigger : MonoBehaviour
{
    [SerializeField] private Light2D _light;
    
    private bool _isTriggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isTriggered == false)
        {
            StartCoroutine(DownLightCoroutine());
        }
    }

    private IEnumerator DownLightCoroutine()
    {
        _isTriggered = true;
        while (_light.intensity > 0)
        {
            _light.intensity -= 1.5f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
