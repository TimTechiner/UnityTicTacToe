using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateMachine
{
    public static IState CurrentState { get; private set; } = new BaseState();

    public static void Update()
    {
        CurrentState.Update();
    }

    public static void Render()
    {
        CurrentState.Render();
    }

    public static void ChangeState(IState state, params object[] parameters)
    {
        if (state == null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter(parameters);
    }
}
