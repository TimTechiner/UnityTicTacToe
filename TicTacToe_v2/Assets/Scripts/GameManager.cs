using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    private void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    private void Update()
    {
        StateMachine.Update();
        StateMachine.Render();
    }

    private void StartGame()
    {
        StateMachine.ChangeState(new PreStartState());
    }
}
