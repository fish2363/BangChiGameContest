using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Npc : MonoBehaviour
{
    
    [SerializeField] protected float _chatRadius;

    [SerializeField] protected LayerMask _whatIsPlayer;
    
    protected GameObject _textBox;
    protected TextMeshPro _textBoxText;
    
    public List<string> NpcChatTexts = new List<string>();
    
    protected virtual void Awake()
    {
        _textBox = transform.Find("TextBox").gameObject;
        _textBoxText = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    
    protected virtual void Start()
    {
        _textBox.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _chatRadius);
    }
#endif
}
