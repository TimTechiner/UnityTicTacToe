using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : Player
{
    private static Dictionary<KeyCode, (int, int)> NumberCoordinatesMap = new()
    {
        [KeyCode.Alpha1] = (0, 0),
        [KeyCode.Alpha2] = (0, 1),
        [KeyCode.Alpha3] = (0, 2),
        [KeyCode.Alpha4] = (1, 0),
        [KeyCode.Alpha5] = (1, 1),
        [KeyCode.Alpha6] = (1, 2),
        [KeyCode.Alpha7] = (2, 0),
        [KeyCode.Alpha8] = (2, 1),
        [KeyCode.Alpha9] = (2, 2),
    };

    private readonly IInputProcessor inputProcessor;

    public RealPlayer(IInputProcessor inputProcessor) : base()
    {
        //if (inputProcessor == null)
        //{
        //    throw new ArgumentNullException(nameof(inputProcessor));
        //}

        this.inputProcessor = inputProcessor;
    }

    public override ICommand MakeTurn(GameField field)
    {
        if (field == null)
        {
            throw new ArgumentNullException(nameof(field));
        }

        ICommand command = new EmptyCommand();

        var buttonIndex = inputProcessor.GetClickedButton();

        if (buttonIndex != (-1, -1))
        {
            if (field[buttonIndex] == Element.None)
            {
                command = new SetFieldElementCommand(field, Element, buttonIndex);
            }
        }

        //var key = inputProcessor.GetKey();

        //if (NumberCoordinatesMap.ContainsKey(key))
        //{
        //    var coordinates = NumberCoordinatesMap[key];

        //    if (field[coordinates] == Element.None)
        //    {
        //        command = new SetFieldElementCommand(field, Element, NumberCoordinatesMap[key]);
        //    }
        //}

        return command;
    }
}
