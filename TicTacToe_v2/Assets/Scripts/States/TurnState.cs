using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : BaseState
{
    private int currentPlayerIndex;
    private Player[] players;
    private GameField field;
    private IInputProcessor inputProcessor;

    public override void Enter(params object[] parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        if (parameters.Length != 4)
        {
            throw new ArgumentException(nameof(parameters));
        }

        if (parameters[0] is not GameField
            || parameters[1] is not Player[]
            || parameters[2] is not int
            || parameters[3] is not IInputProcessor)
        {
            throw new ArgumentException(nameof(parameters));
        }


        field = (GameField)parameters[0];
        players = (Player[])parameters[1];
        currentPlayerIndex = (int)parameters[2];
        inputProcessor = (IInputProcessor)parameters[3];

        field.OnFieldChanged += Field_OnFieldChanged;

        Render();
    }

    public override void Update()
    {
        ICommand command = players[currentPlayerIndex].MakeTurn(field);
        command.Execute();
    }

    public override void Render()
    {
        //Console.WriteLine();

        //FieldRenderer.RenderField(field);
    }

    private void Field_OnFieldChanged(object? sender, EventArgs e)
    {
        field.OnFieldChanged -= Field_OnFieldChanged;

        WinOutcome winner;

        if (IsGameOver(out winner))
        {
            StateMachine.ChangeState(new GameOverState(), winner, field, inputProcessor);
        }
        else
        {
            int newPlayerIndex = (currentPlayerIndex + 1) % 2;
            StateMachine.ChangeState(new TurnState(), field, players, newPlayerIndex, inputProcessor);
        }
    }

    private bool IsGameOver(out WinOutcome winner)
    {
        winner = FieldValidator.GetWinner(field);

        return winner != WinOutcome.None;
    }
}
