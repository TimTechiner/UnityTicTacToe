using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStartState : BaseState
{
    private InterStateUIData data;
    private GameField field;
    private GameFieldUI fieldUI;
    private IInputProcessor inputProcessor;
    private int[] scores;

    public override void Enter(params object[] parameters)
    {
        data = parameters[0] as InterStateUIData;

        fieldUI = data.GameFieldUI;

        field = new GameField();

        inputProcessor = new BasicInputProcessor(fieldUI);

        scores = new int[3];
    }

    public override void Update()
    {
        StateMachine.ChangeState(new GameStartState(), data, field, inputProcessor, scores);
    }
}
