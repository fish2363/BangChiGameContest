using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AutoNpc : Npc
{
    
    [SerializeField] private bool _randomChat;
    
    [SerializeField] private float _waitTime = 2f;
    

    private string _nowText;
    private int _npcIndex;

    private Text _text;

    private bool _intoRangePlayer;
    
    private bool _isTalking = false;
    

    private IEnumerator ChattingCoroutine()
    {
        _isTalking = true;
        if(_randomChat)
            _nowText = NpcChatTexts[UnityEngine.Random.Range(0, NpcChatTexts.Count)];
        else
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
        }
        // 여기까지가 말할 text정하는거

        _textBox.SetActive(true);
        
        yield return _textBoxText.DOText(_nowText, _nowText.Length / 5f).SetEase(Ease.OutCubic).WaitForCompletion();
        
        yield return new WaitForSeconds(_waitTime);

        _textBoxText.text = "";
        _textBox.SetActive(false);

        yield return new WaitForSeconds(_waitTime);
        
        _isTalking = false;
        
        if(_intoRangePlayer)
            StartCoroutine(ChattingCoroutine());
        else
        {
            StopAllCoroutines();
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!_intoRangePlayer && !_isTalking && Physics2D.OverlapCircle(transform.position, _chatRadius, _whatIsPlayer))
        {
            _intoRangePlayer = true;
            StartCoroutine(ChattingCoroutine());
            Debug.Log("코루틴 시작");
        }
        else if(!Physics2D.OverlapCircle(transform.position, _chatRadius, _whatIsPlayer))
        {
            _intoRangePlayer = false;
        }
    }
}
