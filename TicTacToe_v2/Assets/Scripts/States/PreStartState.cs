using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStartState : BaseState
{
    private GameField field;
    private GameFieldUI fieldUI;
    private IInputProcessor inputProcessor;

    public override void Enter(params object[] parameters)
    {
        fieldUI = (GameFieldUI)parameters[0];

        field = new GameField();

        inputProcessor = new BasicInputProcessor(fieldUI);

        StateMachine.ChangeState(new GameStartState(), field, fieldUI, inputProcessor);
    }
}
