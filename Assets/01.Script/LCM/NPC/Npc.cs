using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    [SerializeField] private bool _randomChat;
    
    [SerializeField] private float _waitTime = 2f;

    private GameObject _textBox;
    private TextMeshPro _textBoxText;

    public List<string> NpcChatTexts = new List<string>();

    private string _nowText;
    private int _npcIndex;

    private Text _text;

    private void Awake()
    {
        _textBox = transform.Find("TextBox").gameObject;
        _textBoxText = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        _textBox.SetActive(false);
        StartCoroutine(ChattingCoroutine());
    }

    private IEnumerator ChattingCoroutine()
    {
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
        
        Debug.Log("채팅 치기 시작");
        yield return _textBoxText.DOText(_nowText, _nowText.Length / 5f).SetEase(Ease.OutCubic).WaitForCompletion();
        Debug.Log("채팅 치기 끝");
        
        Debug.Log("기다리기 시작");
        yield return new WaitForSeconds(_waitTime);

        _textBoxText.text = "";
        _textBox.SetActive(false);

        yield return new WaitForSeconds(_waitTime);
        StartCoroutine(ChattingCoroutine());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
