using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    private GameObject selectPlayModePopup;

    // Start is called before the first frame update
    void Start()
    {
        Button backButton = GetComponent<Button>();
        backButton.onClick.AddListener(() =>
        {
            selectPlayModePopup.SetActive(false);
        });
    }
}
