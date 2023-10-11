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
    private Dictionary<string, int> scores;
    private PlayMode playMode;

    public override void Enter(params object[] parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        if (parameters.Length != 7)
        {
            throw new ArgumentException(nameof(parameters));
        }

        data = (InterStateUIData)parameters[0];
        field = (GameField)parameters[1];
        players = (Player[])parameters[2];
        currentPlayerIndex = (int)parameters[3];
        inputProcessor = (IInputProcessor)parameters[4];
        scores = (Dictionary<string, int>)parameters[5];
        playMode = (PlayMode)parameters[6];

        fieldUI = data.GameFieldUI;
        scoreTexts = data.ScoreTexts;
        

        inputProcessor.Reset();
        fieldUI.gameObject.SetActive(true);

        var scoreUI = scoreTexts[0].gameObject.transform.parent.gameObject.transform.parent.gameObject;
        scoreUI.SetActive(true);

        data.CurrentPlayerText.text = $"Turn: Player{currentPlayerIndex + 1}";
        data.CurrentPlayerText.gameObject.SetActive(true);



        field.OnFieldChanged += Field_OnFieldChanged;
    }

    public override void Update()
    {
        ICommand command = players[currentPlayerIndex].MakeTurn(field);
        command.Execute();
    }

    private void Field_OnFieldChanged(object sender, GameField.OnFieldChangedEventArgs e)
    {
        field.OnFieldChanged -= Field_OnFieldChanged;

        fieldUI.Render(e.RowIndex, e.ColumnIndex, e.Element);

        WinOutcome winner;

        List<int> winnerIndexes;

        if (IsGameOver(out winner, out winnerIndexes))
        {
            StateMachine.ChangeState(new GameOverState(), data, field, inputProcessor, winner, currentPlayerIndex, scores, playMode, players, winnerIndexes);
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
                scores,
                playMode);
        }
    }

    private bool IsGameOver(out WinOutcome winner, out List<int> winnerIndex)
    {
        winner = FieldValidator.GetWinner(field);

        winnerIndex = FieldValidator.GetWinnerIndexes(field);

        return winner != WinOutcome.None;
    }
}
