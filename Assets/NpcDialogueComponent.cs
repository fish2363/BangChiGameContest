using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogueComponent : MonoBehaviour
{
    public TextMeshProUGUI npcChatText;
    public CanvasGroup textBoxCanvas;

    public float typingSpeed = 0.05f;

    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ComeNear()
    {
        renderer.sortingLayerName = "Enemy";
        renderer.color = Color.white;
        renderer.flipX = true;
    }
}
