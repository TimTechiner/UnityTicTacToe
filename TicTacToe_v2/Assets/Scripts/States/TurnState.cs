using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : BaseState
{
    private int currentPlayerIndex;
    private Player[] players;
    private GameField field;
    private GameFieldUI fieldUI;
    private IInputProcessor inputProcessor;

    public override void Enter(params object[] parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        //if (parameters.Length != 5)
        //{
        //    throw new ArgumentException(nameof(parameters));
        //}

        //if (parameters[0] is not GameField
        //    || parameters[1] is not Player[]
        //    || parameters[2] is not int
        //    || parameters[3] is not IInputProcessor)
        //{
        //    throw new ArgumentException(nameof(parameters));
        //}


        field = (GameField)parameters[0];
        fieldUI = (GameFieldUI)parameters[1];
        players = (Player[])parameters[2];
        currentPlayerIndex = (int)parameters[3];
        inputProcessor = (IInputProcessor)parameters[4];

        inputProcessor.Reset();
        fieldUI.gameObject.SetActive(true);

        field.OnFieldChanged += Field_OnFieldChanged;

        //Render();
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

    private void Field_OnFieldChanged(object? sender, GameField.OnFieldChangedEventArgs e)
    {
        field.OnFieldChanged -= Field_OnFieldChanged;

        fieldUI.Render(e.RowIndex, e.ColumnIndex, e.Element);

        WinOutcome winner;

        if (IsGameOver(out winner))
        {
            StateMachine.ChangeState(new GameOverState(), winner, field, fieldUI, inputProcessor);
        }
        else
        {
            int newPlayerIndex = (currentPlayerIndex + 1) % 2;
            StateMachine.ChangeState(
                new TurnState(),
                field, 
                fieldUI, 
                players, 
                newPlayerIndex, 
                inputProcessor);
        }
    }

    private bool IsGameOver(out WinOutcome winner)
    {
        winner = FieldValidator.GetWinner(field);

        return winner != WinOutcome.None;
    }
}
