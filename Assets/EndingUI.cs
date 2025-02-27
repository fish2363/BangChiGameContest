using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public void FadeIn()
    {
        print("¿¹?");
        DOTween.To(() =>canvasGroup.alpha, x=>canvasGroup.alpha = x,1f,0.2f);
    }

    public void FadeOut()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0f, 0.2f);
    }
}
