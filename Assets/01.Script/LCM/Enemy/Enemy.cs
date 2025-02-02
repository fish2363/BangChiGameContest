using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D RbCompo { get; private set; }
    protected Dictionary<EnemyStateType, EnemyState> StateEnum = new Dictionary<EnemyStateType, EnemyState>();
    public EnemyStateType currentState;
    public virtual void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
    }
    
    public void TransitionState(EnemyStateType newState)
    {
        StateEnum[currentState].Exit();
        currentState = newState;
        StateEnum[currentState].Enter();
    }
}

public enum EnemyStateType
{
    Idle,
    Move,
    Attack,
    Dead
}
