using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverState : BaseState
{
    private WinOutcome winner;
    private GameField field;
    private GameFieldUI fieldUI;
    private TextMeshProUGUI gameOverText;
    private IInputProcessor inputProcessor;
    private InterStateUIData data;
    private int[] scores;

    private const string WINNER_SHOW_STRING = "Winner is {0}";
    private const string PRESS_TO_RESTART_STRING = "Press {0} to restart a game";
    private const string RESTART_BUTTON = "R";

    public override void Enter(params object[] parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        //if (parameters.Length != 3)
        //{
        //    throw new ArgumentException(nameof(parameters));
        //}

        //if (parameters[0] is not WinOutcome || parameters[1] is not GameField)
        //{
        //    throw new ArgumentException(nameof(parameters));
        //}

        data = (InterStateUIData)parameters[0];
        field = (GameField)parameters[1];
        inputProcessor = (IInputProcessor)parameters[2];
        winner = (WinOutcome)parameters[3];
        scores = (int[])parameters[4];

        fieldUI = data.GameFieldUI;
        gameOverText = data.GameOverText;
        gameOverText.gameObject.SetActive(true);

        switch (winner)
        {
            case WinOutcome.Cross:
                scores[0] += 1;
                break;
            case WinOutcome.Circle:
                scores[1] += 1;
                break;
            case WinOutcome.Draw:
                scores[2] += 1;
                break;
        }

        for (int i = 0; i < scores.Length; i++)
        {
            data.ScoreTexts[i].text = scores[i].ToString();
        }
    }

    public override void Update()
    {
        ICommand command = new EmptyCommand();

        if (inputProcessor.GetKeyDown(KeyCode.R))
        {
            command = new ResetCommand(data, field, inputProcessor, scores);
        }

        command.Execute();
    }

    public override void Render()
    {
        //FieldRenderer.RenderField(field);

        //Console.WriteLine(String.Format(WINNER_SHOW_STRING, winner));
        //Console.WriteLine(String.Format(PRESS_TO_RESTART_STRING, RESTART_BUTTON));
    }

    public override void Exit()
    {
        gameOverText.gameObject.SetActive(false);
    }
}
