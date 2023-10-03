using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISmartStrategy : IPlayStrategy
{
    private readonly static Dictionary<Element, WinOutcome> ElementWinnerMap = new Dictionary<Element, WinOutcome>()
    {
        [Element.Cross] = WinOutcome.Cross,
        [Element.Circle] = WinOutcome.Circle,
    };

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

        var aiWinningPositions = GetAIWinningPositions(field, elementAI);

        var playerWinningPositions = GetPlayerWinningPositions(
            field: field,
            elementPlayer: new List<Element>()
            {
                    Element.Cross,
                    Element.Circle
            }.FirstOrDefault(e => e != elementAI));

        var aiAlmostWinningPositions = GetAIAlmostWinningPositions(field, elementAI);

        if (aiWinningPositions.Any()) return aiWinningPositions.FirstOrDefault();
        if (playerWinningPositions.Any()) return playerWinningPositions.FirstOrDefault();
        if (aiAlmostWinningPositions.Any()) return aiAlmostWinningPositions.FirstOrDefault();

        return new AIRandomStrategy().GetNextTargetCell(field, elementAI);
    }

    private IEnumerable<(int, int)> GetWinningPosition(GameField field, Element element)
    {
        var freeCells = field.GetFreeCells();

        foreach (var freeCell in freeCells)
        {
            var potentialField = field.Clone() as GameField;

            potentialField[freeCell] = element;

            var winner = FieldValidator.GetWinner(potentialField);

            if (ElementWinnerMap[element] == winner) yield return freeCell;
        }
    }

    private IEnumerable<(int, int)> GetAIWinningPositions(GameField field, Element elementAI)
    {
        return GetWinningPosition(field, elementAI);
    }

    private IEnumerable<(int, int)> GetPlayerWinningPositions(GameField field, Element elementPlayer)
    {
        return GetWinningPosition(field, elementPlayer);
    }

    private IEnumerable<(int, int)> GetAIAlmostWinningPositions(GameField field, Element elementAI)
    {
        var freeCells = field.GetFreeCells();

        foreach (var freeCell in freeCells)
        {
            var potentialField = field.Clone() as GameField;

            potentialField[freeCell] = elementAI;

            for (int i = 0; i < potentialField.Size; i++)
            {
                var rowWinner = FieldValidator.GetAlmostWinnerByRow(potentialField, i);
                if (ElementWinnerMap[elementAI] == rowWinner) yield return freeCell;

                var columnWinner = FieldValidator.GetAlmostWinnerByColumn(potentialField, i);
                if (ElementWinnerMap[elementAI] == columnWinner) yield return freeCell;
            }

            var mainDiagonalWinner = FieldValidator.GetAlmostWinnerByMainDiagonal(potentialField);
            if (ElementWinnerMap[elementAI] == mainDiagonalWinner) yield return freeCell;

            var antiDiagonalWinner = FieldValidator.GetAlmostWinnerByAntiDiagonal(potentialField);
            if (ElementWinnerMap[elementAI] == antiDiagonalWinner) yield return freeCell;
        }
    }
}
