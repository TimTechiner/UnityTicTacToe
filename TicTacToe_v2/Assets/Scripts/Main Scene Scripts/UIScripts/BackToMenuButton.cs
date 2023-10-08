using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
    private const string START_MENU_SCENE_NAME = "StartMenu";

    // Start is called before the first frame update
    private void Start()
    {
        Button backToMenuButton = GetComponent<Button>();
        backToMenuButton.onClick.AddListener(GoToMenuScene);
    }

    private void GoToMenuScene()
    {
        SceneManager.LoadScene(START_MENU_SCENE_NAME);
    }
}
