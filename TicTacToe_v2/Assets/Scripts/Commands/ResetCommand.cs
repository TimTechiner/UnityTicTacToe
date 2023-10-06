using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCommand : ICommand
{
    private readonly GameField field;
    private readonly IInputProcessor inputProcessor;
    private readonly InterStateUIData data;
    private int[] scores;

    public ResetCommand(InterStateUIData data, GameField field, IInputProcessor inputProcessor, int[] scores)
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
    }

    public void Execute()
    {
        StateMachine.ChangeState(new GameStartState(), data, field, inputProcessor, scores);
    }
}
