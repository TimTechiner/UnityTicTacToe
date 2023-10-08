using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowPopupButton : MonoBehaviour
{
    [SerializeField]
    private GameObject popup;

    // Start is called before the first frame update
    private void Start()
    {
        Button showPopupButton = GetComponent<Button>();
        showPopupButton.onClick.AddListener(ShowPopup);
    }

    private void ShowPopup()
    {
        popup.SetActive(true);
    }
}
