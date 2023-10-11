using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameFieldUI : MonoBehaviour
{
    [SerializeField]
    private Button[] cells;

    [SerializeField]
    private Sprite crossSprite;

    [SerializeField]
    private Sprite circleSprite;

    [SerializeField]
    private LoadManager loadManager;

    [SerializeField]
    private Image boardImage;

    [SerializeField]
    private GameObject[] strokes;

    private Image[] strokeImages;

    private Image[] cellImages;

    private Animator[] strokeAnimators;

    private const string ANIMATOR_HAS_WINNER = "HasWinner";

    public event EventHandler<OnCellClickedEventArgs> OnCellClicked;

    public class OnCellClickedEventArgs : EventArgs
    {
        public int RowIndex { get; }
        public int ColumnIndex { get; }

        public OnCellClickedEventArgs(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        cellImages = cells.Select(c => c.GetComponent<Image>()).ToArray();

        strokeImages = strokes.Select(s => s.GetComponent<Image>()).ToArray();

        strokeAnimators = strokes.Select(s => s.GetComponent<Animator>()).ToArray();

        foreach (var cellImage in cellImages)
        {
            cellImage.color = new Color(
                loadManager.CurrentColor.r,
                loadManager.CurrentColor.g,
                loadManager.CurrentColor.b,
                0);
        }

        foreach (var strokeImage in strokeImages)
        {
            strokeImage.color = loadManager.CurrentColor;
        }

        boardImage.color = loadManager.CurrentColor;

        for (int i = 0; i < cells.Length; i++)
        {
            var rowIndex = i / GameField.FIELDSIZE;
            var columnIndex = i % GameField.FIELDSIZE;
            var j = i;
            OnCellClickedEventArgs args = new OnCellClickedEventArgs(rowIndex, columnIndex);
            cells[i].onClick.AddListener(() => {
                OnCellClicked?.Invoke(this, args);
            });
        }
    }

    public void ResetField()
    {
        if (cellImages == null) return;

        for (int i = 0; i < cellImages.Length; i++)
        {
            cellImages[i].sprite = null;
            cellImages[i].color =
                new Color(
                    cellImages[i].color.r,
                    cellImages[i].color.g,
                    cellImages[i].color.b,
                    0);
        }
    }

    public void SetEnabledHorizontalStroke(int rowIndex, bool enabled)
    {
        if (rowIndex >= 0)
        {
            strokes[rowIndex].SetActive(enabled);
            strokeAnimators[rowIndex].SetBool(ANIMATOR_HAS_WINNER, enabled);
        }
    }

    public void SetEnabledVerticalStroke(int columnIndex, bool enabled)
    {
        if (columnIndex >= 0)
        {
            strokes[GameField.FIELDSIZE + columnIndex].SetActive(enabled);
            strokeAnimators[GameField.FIELDSIZE + columnIndex].SetBool(ANIMATOR_HAS_WINNER, enabled);
        }
    }

    public void RenderMainDiagonalStroke(bool enabled)
    {
        strokes[2 * GameField.FIELDSIZE].SetActive(enabled);
        strokeAnimators[2 * GameField.FIELDSIZE].SetBool(ANIMATOR_HAS_WINNER, enabled);
    }

    public void RenderAntiDiagonalStroke(bool enabled)
    {
        strokes[2 * GameField.FIELDSIZE + 1].SetActive(enabled);
        strokeAnimators[2 * GameField.FIELDSIZE + 1].SetBool(ANIMATOR_HAS_WINNER, enabled);
    }

    public void Render(int rowIndex, int columnIndex, Element insertedElement)
    {
        int lineIndex = rowIndex * GameField.FIELDSIZE + columnIndex;

        switch (insertedElement)
        {
            case Element.None:
                cellImages[lineIndex].sprite = null;
                cellImages[lineIndex].color = 
                    new Color(
                        cellImages[lineIndex].color.r, 
                        cellImages[lineIndex].color.g, 
                        cellImages[lineIndex].color.b, 
                        0);
                break;
            case Element.Cross:
                cellImages[lineIndex].sprite = crossSprite;
                cellImages[lineIndex].color =
                    new Color(
                        cellImages[lineIndex].color.r,
                        cellImages[lineIndex].color.g,
                        cellImages[lineIndex].color.b,
                        255);
                break;
            case Element.Circle:
                cellImages[lineIndex].sprite = circleSprite;
                cellImages[lineIndex].color =
                    new Color(
                        cellImages[lineIndex].color.r,
                        cellImages[lineIndex].color.g,
                        cellImages[lineIndex].color.b,
                        255);
                break;
            default:
                throw new Exception();
        }
    }
}
