using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour,IEntityComponent
{
    [Header("Error")]
    [SerializeField] private GameEventChannelSO UIChannel;
    [SerializeField] private TextMeshProUGUI errorTextUI;

    [Header("Help")]
    [SerializeField] private TextMeshProUGUI helpTextUI;

    private Player _player;
    private bool isHelpText;

    public void Initialize(Entity entity)
    {
        UIChannel.AddListener<TextEvent>(HandleTextEvent);
        _player = entity as Player;
    }

    
    private void Update()
    {
        if(isHelpText && Input.GetKeyDown(KeyCode.Space))
        {
            isHelpText =false;
            StartCoroutine(FadeOutTextRoutine(helpTextUI));
        }
    }


    private void HandleTextEvent(TextEvent obj)
    {
        if(obj.textType == TextType.Error)
        {
            errorTextUI.color = Color.red;
            errorTextUI.text = $"¿À·ù : {obj.Text}";
            errorTextUI.DOFade(1, 1f);

            if (obj.isDefunct)
            {
                StartCoroutine(FadeOutTextRoutine(errorTextUI));
            }
        }
        else if (obj.textType == TextType.Help)
        {
            helpTextUI.text = obj.Text;
            helpTextUI.DOFade(1, 1f);
            isHelpText = true;
            StartCoroutine(FadeOutTextRoutine(helpTextUI));
        }
        
    }

    private IEnumerator FadeOutTextRoutine(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(3f);
        text.DOFade(0, 1f);
    }

    
}
