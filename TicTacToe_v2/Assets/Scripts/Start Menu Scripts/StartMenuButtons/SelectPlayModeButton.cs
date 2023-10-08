using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPlayModeButton : MonoBehaviour
{
    [SerializeField]
    private PlayerMode playMode;

    private const string MAIN_SCENE_NAME = "SampleScene";

    // Start is called before the first frame update
    private void Start()
    {
        Button selectPlayModeButton = GetComponent<Button>();
        selectPlayModeButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        GameManager.PlayerMode = playMode;

        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
}
