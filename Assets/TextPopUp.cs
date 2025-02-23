using TMPro;
using UnityEngine;
using DG.Tweening;

public class TextPopUp : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        TextHide();
    }

    public void TextShow()
    {
        text.DOFade(1,1);
    }

    public void TextHide()
    {
        text.DOFade(0, 1);
    }
}
