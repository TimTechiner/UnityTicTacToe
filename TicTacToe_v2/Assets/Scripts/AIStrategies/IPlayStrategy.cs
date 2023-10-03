using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayStrategy
{
    public (int, int) GetNextTargetCell(GameField field, Element elementAI);
}
