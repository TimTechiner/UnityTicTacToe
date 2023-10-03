using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    private Button startGameButton;

    private const string MAIN_SCENE_NAME = "SampleScene";

    // Start is called before the first frame update
    private void Start()
    {
        startGameButton = GetComponent<Button>();
        startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
}
