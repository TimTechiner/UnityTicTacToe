using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameStartState : BaseState
{
    private const int N_PLAYERS = 2;
    private const string SELECT_MODE_STRING = "Please select a game mode.\nPress '1' for SinglePlayer.\nPress '2' for MultiPlayer";
    private GameField field;
    private GameFieldUI fieldUI;
    private Player[] players;
    private PlayerMode mode;
    private int firstPlayerIndex;
    private IInputProcessor inputProcessor;
    private int[] scores;

    private InterStateUIData data;

    public Player[] Players => players;
    public PlayerMode Mode => mode;

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
        scores = (int[])parameters[3];

        fieldUI = data.GameFieldUI;

        players = new Player[N_PLAYERS];

        //Render();
    }

    public override void Update()
    {
        field.Reset();

        fieldUI.ResetField();

        mode = PlayerMode.SinglePlayer;

        InitializePlayers();
        SetPlayersElements();
        SetFirstPlayer();
        StateMachine.ChangeState(
            new TurnState(), 
            data,
            field, 
            players, 
            firstPlayerIndex, 
            inputProcessor,
            scores);

        //var key = inputProcessor.GetKey();

        //if (key == KeyCode.Alpha1 || key == KeyCode.Alpha2)
        //{
        //    mode = (key == KeyCode.Alpha1) ? PlayerMode.SinglePlayer : PlayerMode.MultiPlayer;

        //    InitializePlayers();
        //    SetPlayersElements();
        //    SetFirstPlayer();
        //    StateMachine.ChangeState(new TurnState(), field, players, firstPlayerIndex, inputProcessor);
        //}
    }

    public override void Render()
    {
        Console.WriteLine(SELECT_MODE_STRING);
    }

    private void InitializePlayers()
    {
        switch (mode)
        {
            case PlayerMode.SinglePlayer:
                players[0] = new RealPlayer(inputProcessor);
                players[1] = new AIPlayer() { Strategy = new AISmartStrategy() };
                break;
            case PlayerMode.MultiPlayer:
                players[0] = new RealPlayer(inputProcessor);
                players[1] = new RealPlayer(inputProcessor);
                break;
            default:
                throw new PlayerModeInvalidException("Player Mode is not Recognized");
        }
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
