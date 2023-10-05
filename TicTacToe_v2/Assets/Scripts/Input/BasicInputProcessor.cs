using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInputProcessor : IInputProcessor
{
    private (int, int) clickedCell = (-1, -1);

    public BasicInputProcessor() { }

    public BasicInputProcessor(GameFieldUI gameFieldUI)
    {
        gameFieldUI.OnCellClicked += GameFieldUI_OnCellClicked;
    }

    private void GameFieldUI_OnCellClicked(object sender, GameFieldUI.OnCellClickedEventArgs e)
    {
        clickedCell = (e.RowIndex, e.ColumnIndex);
    }

    public (int, int) GetClickedButton()
    {
        return clickedCell;
    }

    public void Reset()
    {
        clickedCell = (-1, -1);
    }

    public KeyCode GetKey()
    {
        return KeyCode.None;
    }
}
