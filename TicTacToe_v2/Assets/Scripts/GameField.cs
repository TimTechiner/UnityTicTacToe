using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class GameField
{
    public const int FIELDSIZE = 3;
    private readonly Element[,] _field = new Element[FIELDSIZE, FIELDSIZE];

    public bool IsFilled
    {
        get
        {
            return _field.Cast<Element>().All(e => e != Element.None);
        }
    }

    public bool IsEmpty
    {
        get
        {
            return _field.Cast<Element>().All(e => e == Element.None);
        }
    }

    public int Size => FIELDSIZE;

    public event EventHandler<EventArgs>? OnFieldChanged;

    public Element this[(int, int) position]
    {
        get
        {
            return _field[position.Item1, position.Item2];
        }
        set
        {
            _field[position.Item1, position.Item2] = value;
            OnFieldChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public GameField()
    {
        InitializeField();
    }

    private GameField(Element[,] field)
    {
        _field = field;
    }

    public object Clone()
    {
        return new GameField(_field.Clone() as Element[,]);
    }

    public void Reset()
    {
        InitializeField();
    }

    public IEnumerable<(int, int)> GetFreeCells()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(1); j++)
            {
                if (_field[i, j] == Element.None) yield return (i, j);
            }
        }
    }

    public IEnumerable<Element> GetRow(int rowIndex)
    {
        return GetVector(rowIndex, true);
    }

    public IEnumerable<Element> GetColumn(int columnIndex)
    {
        return GetVector(columnIndex, false);
    }

    public IEnumerable<Element> GetMainDiagonal()
    {
        return GetDiagonal(true);
    }

    public IEnumerable<Element> GetAntiDiagonal()
    {
        return GetDiagonal(false);
    }

    private IEnumerable<Element> GetDiagonal(bool isMain)
    {
        IEnumerable<(int, int)> cellsCoordinates;

        if (isMain)
        {
            cellsCoordinates = Enumerable.Range(0, Size).Select(e => (e, e));
        }
        else
        {
            cellsCoordinates = Enumerable.Range(0, Size).Select(e => (Size - 1 - e, e));
        }

        foreach (var cellCoordinates in cellsCoordinates)
        {
            yield return _field[cellCoordinates.Item1, cellCoordinates.Item2];
        }
    }

    private IEnumerable<Element> GetVector(int index, bool isRow)
    {
        for (int k = 0; k < Size; k++)
        {
            if (isRow)
            {
                yield return _field[index, k];
            }
            else
            {
                yield return _field[k, index];
            }
        }
    }

    private void InitializeField()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(1); j++)
            {
                _field[i, j] = Element.None;
            }
        }
    }
}
