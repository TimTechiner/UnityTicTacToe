using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameFieldUIObject;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private TextMeshProUGUI[] scoreTexts;

    [SerializeField]
    private GameObject exitToMenuButtonObject;

    [SerializeField]
    private TextMeshProUGUI currentPlayerText;

    public static PlayerMode PlayerMode { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    private void Update()
    {
        StateMachine.Update();
    }

    private void StartGame()
    {
        InterStateUIData data = new InterStateUIData();
        data.GameFieldUI = gameFieldUIObject.GetComponent<GameFieldUI>();
        data.ExitToMenuButton = exitToMenuButtonObject;
        data.GameOverText = gameOverText;
        data.ScoreTexts = scoreTexts;
        data.CurrentPlayerText = currentPlayerText;

        StateMachine.ChangeState(new PreStartState(), data, PlayerMode);
    }
}
