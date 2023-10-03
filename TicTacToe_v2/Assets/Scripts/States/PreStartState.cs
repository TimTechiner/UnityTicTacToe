using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStartState : BaseState
{
    private GameField field;

    public void Enter(params object[] parameters)
    {
        field = new GameField();
        StateMachine.ChangeState(new GameStartState(), field);
    }
}
