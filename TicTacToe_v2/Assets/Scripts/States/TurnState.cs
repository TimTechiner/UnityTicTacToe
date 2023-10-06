using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnState : BaseState
{
    private int currentPlayerIndex;
    private Player[] players;
    private GameField field;
    private GameFieldUI fieldUI;
    private TextMeshProUGUI[] scoreTexts;
    private IInputProcessor inputProcessor;
    private InterStateUIData data;
    private int[] scores;

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

        data = (InterStateUIData)parameters[0];
        field = (GameField)parameters[1];
        players = (Player[])parameters[2];
        currentPlayerIndex = (int)parameters[3];
        inputProcessor = (IInputProcessor)parameters[4];
        scores = (int[])parameters[5];

        fieldUI = data.GameFieldUI;
        scoreTexts = data.ScoreTexts;

        inputProcessor.Reset();
        fieldUI.gameObject.SetActive(true);

        var scoreUI = scoreTexts[0].gameObject.transform.parent.gameObject.transform.parent.gameObject;
        scoreUI.SetActive(true);

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
            StateMachine.ChangeState(new GameOverState(), data, field, inputProcessor, winner, scores);
        }
        else
        {
            int newPlayerIndex = (currentPlayerIndex + 1) % 2;
            StateMachine.ChangeState(
                new TurnState(),
                data,
                field, 
                players, 
                newPlayerIndex, 
                inputProcessor,
                scores);
        }
    }

    private bool IsGameOver(out WinOutcome winner)
    {
        winner = FieldValidator.GetWinner(field);

        return winner != WinOutcome.None;
    }
}
