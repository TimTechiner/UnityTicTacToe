using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class GameOverState : BaseState
{
    private WinOutcome winner;
    private List<int> winnerIndexes;
    private int lastPlayerIndex;
    private GameField field;
    private GameFieldUI fieldUI;
    private TextMeshProUGUI gameOverText;
    private IInputProcessor inputProcessor;
    private InterStateUIData data;
    private Dictionary<string, int> scores;
    private PlayMode playMode;
    private Player[] players;

    private const string WINNER_SHOW_STRING = "Winner is {0}";
    private const string PRESS_TO_RESTART_STRING = "Press {0} to restart a game";
    private const string RESTART_BUTTON = "R";

    public override void Enter(params object[] parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        if (parameters.Length != 9)
        {
            throw new ArgumentException(nameof(parameters));
        }

        data = (InterStateUIData)parameters[0];
        field = (GameField)parameters[1];
        inputProcessor = (IInputProcessor)parameters[2];
        winner = (WinOutcome)parameters[3];
        lastPlayerIndex = (int)parameters[4];
        scores = (Dictionary<string, int>)parameters[5];
        playMode = (PlayMode)parameters[6];
        players = (Player[])parameters[7];
        winnerIndexes = (List<int>)parameters[8];

        fieldUI = data.GameFieldUI;
        gameOverText = data.GameOverText;
        gameOverText.gameObject.SetActive(true);

        if (winner == WinOutcome.Draw)
        {
            scores["Draw"] += 1;
        }
        else
        {
            if (lastPlayerIndex == 0)
            {
                scores["Player1"] += 1;
            }
            else if (lastPlayerIndex == 1)
            {
                scores["Player2"] += 1;
            }
        }

        data.ScoreTexts[0].text = scores["Player1"].ToString();
        data.ScoreTexts[2].text = scores["Draw"].ToString();
        data.ScoreTexts[1].text = scores["Player2"].ToString();

        SetEnabledStrokes(true);
    }

    private void SetEnabledStrokes(bool enabled)
    {
        var horizontalIndex = winnerIndexes[0];
        var verticalIndex = winnerIndexes[1];
        var mainDiagonalIndex = winnerIndexes[2];
        var antiDiagonalIndex = winnerIndexes[3];

        if (horizontalIndex >= 0)
        {
            fieldUI.SetEnabledHorizontalStroke(horizontalIndex, enabled);
        }

        if (verticalIndex >= 0)
        {
            fieldUI.SetEnabledVerticalStroke(verticalIndex, enabled);
        }

        if (mainDiagonalIndex >= 0)
        {
            fieldUI.RenderMainDiagonalStroke(enabled);
        }

        if (antiDiagonalIndex >= 0)
        {
            fieldUI.RenderAntiDiagonalStroke(enabled);
        }
    }

    public override void Update()
    {
        ICommand command = new EmptyCommand();

        if (inputProcessor.GetKeyDown(KeyCode.R))
        {
            command = new ResetCommand(data, field, inputProcessor, scores, playMode, players);
        }

        command.Execute();
    }

    public override void Exit()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        if (winnerIndexes != null)
        {
            SetEnabledStrokes(false);
        }
    }
}
