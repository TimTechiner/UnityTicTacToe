using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : BaseState
{
    private WinOutcome winner;
    private GameField field;
    private GameFieldUI fieldUI;
    private IInputProcessor inputProcessor;

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

        winner = (WinOutcome)parameters[0];
        field = (GameField)parameters[1];
        fieldUI = (GameFieldUI)parameters[2];
        inputProcessor = (IInputProcessor)parameters[3];
    }

    public override void Update()
    {
        var key = inputProcessor.GetKey();
        ICommand command = new EmptyCommand();

        if (key == KeyCode.R)
        {
            command = new ResetCommand(field, inputProcessor);
        }

        command.Execute();
    }

    public override void Render()
    {
        //FieldRenderer.RenderField(field);

        //Console.WriteLine(String.Format(WINNER_SHOW_STRING, winner));
        //Console.WriteLine(String.Format(PRESS_TO_RESTART_STRING, RESTART_BUTTON));
    }
}
