using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStartState : BaseState
{
    private const int N_PLAYERS = 2;

    private InterStateUIData data;
    private GameField field;
    private GameFieldUI fieldUI;
    private IInputProcessor inputProcessor;
    private Dictionary<string, int> scores;
    private Player[] players;

    private PlayerMode playMode;

    public override void Enter(params object[] parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        if (parameters.Length != 2)
        {
            throw new ArgumentException(nameof(parameters));
        }

        data = (InterStateUIData)parameters[0];

        playMode = (PlayerMode)parameters[1];

        fieldUI = data.GameFieldUI;

        field = new GameField();

        inputProcessor = new BasicInputProcessor(fieldUI);

        InitializePlayers();
        InitializeScores();
    }

    public override void Update()
    {
        StateMachine.ChangeState(new GameStartState(), data, field, inputProcessor, scores, playMode, players);
    }

    private void InitializePlayers()
    {
        players = new Player[N_PLAYERS];

        switch (playMode)
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

    private void InitializeScores()
    {
        scores = new Dictionary<string, int>();
        scores["Draw"] = 0;
        scores["Player1"] = 0;
        scores["Player2"] = 0;
    }
}
