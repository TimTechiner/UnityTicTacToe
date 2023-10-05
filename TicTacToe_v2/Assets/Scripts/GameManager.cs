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

    private GameFieldUI gameFieldUI;

    // Start is called before the first frame update
    private void Start()
    {
        gameFieldUI = gameFieldUIObject.GetComponent<GameFieldUI>();

        StartGame();
    }

    // Update is called once per frame
    private void Update()
    {
        StateMachine.Update();
        //StateMachine.Render();
    }

    private void StartGame()
    {
        StateMachine.ChangeState(new PreStartState(), gameFieldUI);
    }
}
