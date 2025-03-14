using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void DeathScreenFadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 1).OnComplete(()=> Time.timeScale = 0f);
    }

    public void ReStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FinalBossSceneStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FinalBossSceneReStart");
    }

    public void Trash()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
