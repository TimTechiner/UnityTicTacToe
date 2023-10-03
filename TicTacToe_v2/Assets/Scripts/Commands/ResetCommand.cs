using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCommand : ICommand
{
    private readonly GameField field;
    private readonly IInputProcessor inputProcessor;

    public ResetCommand(GameField field, IInputProcessor inputProcessor)
    {
        if (field == null)
        {
            throw new ArgumentNullException(nameof(field));
        }

        if (inputProcessor == null)
        {
            throw new ArgumentNullException(nameof(inputProcessor));
        }

        this.field = field;
        this.inputProcessor = inputProcessor;
    }

    public void Execute()
    {
        StateMachine.ChangeState(new GameStartState(), field, inputProcessor);
    }
}
