using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameStartState : BaseState
{
    private const int N_PLAYERS = 2;
    private GameField field;
    private GameFieldUI fieldUI;
    private Player[] players;
    private PlayerMode mode;
    private int firstPlayerIndex;
    private IInputProcessor inputProcessor;
    private Dictionary<string,int> scores;

    private InterStateUIData data;

    public override void Enter(params object[] parameters)
    {
        //if (parameters == null)
        //{
        //    throw new ArgumentNullException(nameof(parameters));
        //}

        //if (parameters.Length != 3)
        //{
        //    throw new ArgumentException(nameof(parameters));
        //}

        //if (parameters[0] is not GameField || parameters[1] is not IInputProcessor)
        //{
        //    throw new ArgumentException(nameof(parameters));
        //}

        data = parameters[0] as InterStateUIData;
        field = (GameField)parameters[1];
        inputProcessor = (IInputProcessor)parameters[2];
        scores = (Dictionary<string, int>)parameters[3];
        mode = (PlayerMode)parameters[4];
        players = (Player[])parameters[5];

        fieldUI = data.GameFieldUI;
    }

    public override void Update()
    {
        field.Reset();
        fieldUI.ResetField();

        SetPlayersElements();
        SetFirstPlayer();

        StateMachine.ChangeState(
            new TurnState(), 
            data,
            field, 
            players, 
            firstPlayerIndex, 
            inputProcessor,
            scores,
            mode);
    }

    private void SetPlayersElements()
    {
        var crossPlayerIndex = Random.Range(0, N_PLAYERS);
        var circlePlayerIndex = Enumerable.Range(0, N_PLAYERS).FirstOrDefault(e => e != crossPlayerIndex);

        players[crossPlayerIndex].Element = Element.Cross;
        players[circlePlayerIndex].Element = Element.Circle;
    }

    private void SetFirstPlayer()
    {
        firstPlayerIndex = Random.Range(0, N_PLAYERS);
    }
}
