using UnityEngine;
using TMPro;
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour, IEntityComponent
{
    [SerializeField] private GameEventChannelSO dialogueChannel;

    private Entity _entity;
    private Player _player;
    private EntityMover _mover;
    private EntityRenderer _renderer;

    public string[] currentDialogue { get; private set; }

    private TextMeshProUGUI chatText;
    private CanvasGroup textBoxCanvas;
    private Image textBoxImage;

    bool isSkip;
    int talkNum;
    float typingSpeed = 0.05f;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _player = _entity as Player;
        _mover = _entity.GetCompo<EntityMover>();
        _renderer = _entity.GetCompo<EntityRenderer>();

        _renderer.OnFlip += Flip;
        _player.PlayerInput.OnAttackKeyPressed += HandleClick;
        dialogueChannel.AddListener<StartAConversation>(HandleSpeak);

        textBoxCanvas = GetComponentInChildren<CanvasGroup>();
        textBoxImage = textBoxCanvas.GetComponentInChildren<Image>();
        chatText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        _renderer.OnFlip -= Flip;
        _player.PlayerInput.OnAttackKeyPressed -= HandleClick;
        dialogueChannel.RemoveListener<StartAConversation>(HandleSpeak);
    }

    private void HandleClick()
    {
        if (!isSkip) return;

        isSkip = false;
        NextChat();
    }

    private void NextChat()
    {
        chatText.text = null;
        talkNum++;

        if (talkNum == currentDialogue.Length)
        {
            EndTalk();
            return;
        }

        StartCoroutine(TypingRoutine(currentDialogue[talkNum]));
    }

    private void Flip(bool LeftOrRight)
    {
        if(LeftOrRight)
            textBoxImage.transform.Rotate(0,180f,0);
        else
            textBoxImage.transform.Rotate(0, 0, 0);
    }


    private void EndTalk()
    {
        _mover.CanManualMove = true;
        _player.isBannedAttack = false;
        talkNum = 0;
        HideChatBox();
    }
    public void ShowChatBox()
    {
        DOTween.To(() => textBoxCanvas.alpha, x => textBoxCanvas.alpha = x, 1, 0.2f);
    }
    private void HideChatBox()
    {
        DOTween.To(()=> textBoxCanvas.alpha,x => textBoxCanvas.alpha = x,0,0.2f);
    }

    private void HandleSpeak(StartAConversation events)
    {
        if (events.isStop)
        {
            _mover.CanManualMove = false;
            _mover.StopImmediately(true);
            _renderer.SeeRightDirection();
        }

        _player.isBannedAttack = true;
        currentDialogue = events.dialogue;
        StartCoroutine(TypingRoutine(currentDialogue[talkNum]));
    }

    private IEnumerator TypingRoutine(string talk)
    {

        ShowChatBox();
        chatText.text = null;

        if (talk.Contains("  ")) talk.Replace("  ", "\n");

        for(int i =0; i<talk.Length; i++)
        {
            chatText.text += talk[i];
            yield return new WaitForSeconds(typingSpeed);
        }
        isSkip = true;
    }
}
