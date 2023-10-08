using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    private GameObject popup;

    // Start is called before the first frame update
    void Start()
    {
        Button backButton = GetComponent<Button>();
        backButton.onClick.AddListener(() =>
        {
            popup.SetActive(false);
        });
    }
}
