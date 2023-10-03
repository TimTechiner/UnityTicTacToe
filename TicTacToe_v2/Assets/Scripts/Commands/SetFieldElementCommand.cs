using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFieldElementCommand : ICommand
{
    private readonly GameField field;
    private readonly Element element;
    private readonly (int, int) position;

    public SetFieldElementCommand(GameField field, Element element, (int, int) position)
    {
        if (field == null)
        {
            throw new ArgumentNullException(nameof(field));
        }

        this.field = field;
        this.element = element;
        this.position = position;
    }

    public void Execute()
    {
        if (field[position] == Element.None)
        {
            field[position] = element;
        }
    }
}
