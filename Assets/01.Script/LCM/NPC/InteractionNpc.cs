using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionNpc : Npc
{
    [SerializeField] private GameObject _interactionKey;

    protected override void Start()
    {
        base.Start();
        _interactionKey.SetActive(false);
    }

    private IEnumerator ChattingCoroutine()
    {
        _isTalking = true;
        _interactionKey.SetActive(false);
        for (int i = 0; i < NpcChatTexts.Count; i++)
        {
            if (_npcIndex >= NpcChatTexts.Count)
            {
                _npcIndex = 0;
                _nowText = NpcChatTexts[_npcIndex];
                _npcIndex++;
            }
            else
            {
                _nowText = NpcChatTexts[_npcIndex];
                _npcIndex++;
            }
            // 여기까지가 말할 text정하는거

            _textBox.SetActive(true);

            yield return _textBoxText.DOText(_nowText, _nowText.Length / 5f).SetEase(Ease.OutCubic).WaitForCompletion();

            yield return new WaitForSeconds(_waitTime);

            _textBoxText.text = "";
            _textBox.SetActive(false);

            yield return new WaitForSeconds(_waitTime);
        }

        _isTalking = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!_isTalking && Physics2D.OverlapCircle(transform.position, _chatRadius, _whatIsPlayer))
        {
            _interactionKey.SetActive(true);
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                StartCoroutine(ChattingCoroutine());
            }
        }
        else if (!Physics2D.OverlapCircle(transform.position, _chatRadius, _whatIsPlayer))
        {
            _interactionKey.SetActive(false);
        }
    }
}