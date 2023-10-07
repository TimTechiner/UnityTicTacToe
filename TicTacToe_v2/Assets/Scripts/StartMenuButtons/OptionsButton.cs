using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsPopup;

    // Start is called before the first frame update
    private void Start()
    {
        Button optionsButton = GetComponent<Button>();
        optionsButton.onClick.AddListener(ShowOptionsPopup);
    }

    private void ShowOptionsPopup()
    {
        optionsPopup.SetActive(true);
    }
}
