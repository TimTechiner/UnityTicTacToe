using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCommand : ICommand
{
    private readonly GameField field;
    private readonly IInputProcessor inputProcessor;
    private readonly InterStateUIData data;
    private readonly Dictionary<string, int> scores;
    private readonly PlayMode playMode;
    private readonly Player[] players;

    public ResetCommand(InterStateUIData data, GameField field, IInputProcessor inputProcessor, Dictionary<string, int> scores, PlayMode playMode, Player[] players)
    {
        if (field == null)
        {
            throw new ArgumentNullException(nameof(field));
        }

        if (inputProcessor == null)
        {
            throw new ArgumentNullException(nameof(inputProcessor));
        }

        this.data = data;
        this.field = field;
        this.inputProcessor = inputProcessor;
        this.scores = scores;
        this.playMode = playMode;
        this.players = players;
    }

    public void Execute()
    {
        StateMachine.ChangeState(new GameStartState(), data, field, inputProcessor, scores, playMode, players);
    }
}
