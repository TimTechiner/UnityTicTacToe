using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    private IPlayStrategy strategy = new AIRandomStrategy();
    public IPlayStrategy Strategy
    {
        get => strategy;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            strategy = value;
        }
    }
    public override ICommand MakeTurn(GameField field)
    {
        if (field == null)
        {
            throw new ArgumentNullException(nameof(field));
        }

        var targetCell = Strategy.GetNextTargetCell(field, Element);

        return new SetFieldElementCommand(field, Element, targetCell);
    }
}
