using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterStateUIData
{
    public GameFieldUI GameFieldUI { get; set; }
    public GameObject ExitToMenuButton { get; set; }
    public TextMeshProUGUI GameOverText { get; set; }
    public TextMeshProUGUI[] ScoreTexts { get; set; }
    public TextMeshProUGUI CurrentPlayerText { get; set; }
}
