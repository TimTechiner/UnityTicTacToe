using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIRandomStrategy : IPlayStrategy
{
    public (int, int) GetNextTargetCell(GameField field, Element elementAI)
    {
        if (field == null)
        {
            throw new ArgumentNullException(nameof(field));
        }

        if (elementAI == Element.None)
        {
            throw new PlayableElementException($"AI cannot play with element : {elementAI}");
        }

        var freeCells = field.GetFreeCells();

        if (!freeCells.Any())
        {
            throw new FieldFilledException("Field is already filled");
        }

        var index = Random.Range(0, freeCells.Count());
        var randomFreeCell = freeCells.ToList()[index];

        return randomFreeCell;
    }
}
