using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EscFunction : MonoBehaviour
{
    [SerializeField] private Image _escPanel;
    [SerializeField] private Image _checkPanel;
    [SerializeField] private Image _whitePanel;

    private bool _isEnd = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowEscPanel();
        }
    }

    public void ShowEscPanel()
    {
        // 설정과 상호작용했을때 창 띄우는 메소드
        if(!IsEnd()) return;
        
        _isEnd = false;
        _whitePanel.DOFade(0.5f, 1f);
        _escPanel.rectTransform.DOMoveY(540f, 1.5f).SetEase(Ease.InOutBounce).onComplete = () =>
        {
            _isEnd = true;
        };
    }

    public void CloseEscPanel()
    {
        // Esc창 닫는 메소드
        if(!IsEnd()) return;
        
        _isEnd = false;
        _whitePanel.DOFade(0f, 1f);
        _escPanel.rectTransform.DOMoveY(-460f, 1.5f).SetEase(Ease.InOutBounce).onComplete = () =>
        {
            _isEnd = true;
        };;
    }

    public void CheckExitGamePanel()
    {
        // 게임 나가기 버튼 누르면 확인창 띄우는 메소드
        
        if(!IsEnd()) return;
        
        _isEnd = false;
        _whitePanel.DOColor(Color.red, 1.5f);
        _whitePanel.DOFade(0.5f, 1.5f);
        _checkPanel.rectTransform.DOMoveY(540f, 1.5f).SetEase(Ease.InBounce).onComplete = () =>
        {
            _isEnd = true;
        };;
    }

    public void ExitGameBtn()
    {
        // 게임 종료 메소드
    }

    public void CloseExitGamePanel()
    {
        // 확인창 닫는 메소드
        if(!IsEnd()) return;
        
        _isEnd = false;
        _whitePanel.DOColor(Color.white, 1.5f);
        _whitePanel.DOFade(0.5f, 1.5f);
        _checkPanel.rectTransform.DOMoveY(-460f, 1.5f).SetEase(Ease.InBounce).onComplete = () =>
        {
            _isEnd = true;
        };;
    }

    private bool IsEnd()
    {
        return _isEnd;
    }
}
