using System;
using UnityEngine;

public class DrawSlime : Enemy
{
    public override void Awake()
    {
        base.Awake();
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"DrawSlime_{enumName}State");
                EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                StateEnum.Add(stateType, state);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        TransitionState(EnemyStateType.Idle);
    }
}
