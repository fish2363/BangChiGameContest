using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMarchine : MonoBehaviour
{
    private StateEnum currentState;
    private Dictionary<StateEnum, EntityState> _states = new Dictionary<StateEnum, EntityState>();
    public StateMarchine(Entity entity)
    {
        foreach(StateEnum stateEnum in Enum.GetValues(typeof(StateEnum)))
        {
            Type t = Type.GetType($"{stateEnum}State");
            EntityState entityState = Activator.CreateInstance(t,entity)as EntityState;
            _states.Add(stateEnum,entityState);
        }
    }

    public void ChangeState(StateEnum newState)
    {
        _states[currentState]?.Exit();
    }
}
