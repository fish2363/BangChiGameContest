using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditLogic : MonoBehaviour
{
    [SerializeField]
    private Image blackPanel;

    [SerializeField]
    private Image credit;
    void Start()
    {
        //AudioManager.Instance.PlaySound2D("EndingBGM",0,true,SoundType.BGM);
        EndingScene();
    }

    private void EndingScene()
    {
        blackPanel.DOFade(1, 0);
        StartCoroutine(CreditRoutine());
    }

    private IEnumerator CreditRoutine()
    {
        yield return new WaitForSeconds(1f);
        credit.GetComponent<RectTransform>().DOLocalMoveY(5000, 30);
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene("StartMenu");
    }
}
