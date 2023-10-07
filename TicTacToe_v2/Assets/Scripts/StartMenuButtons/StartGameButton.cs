using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField]
    private GameObject selectPlayModePopup;

    // Start is called before the first frame update
    private void Start()
    {
        Button startGameButton = GetComponent<Button>();
        startGameButton.onClick.AddListener(ShowSelectPlayModePopup);
    }

    private void ShowSelectPlayModePopup()
    {
        selectPlayModePopup.SetActive(true);
    }
}
