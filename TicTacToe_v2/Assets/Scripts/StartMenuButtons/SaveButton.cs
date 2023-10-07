using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField]
    private LoadManager loadManager;

    // Start is called before the first frame update
    private void Start()
    {
        Button saveButton = GetComponent<Button>();
        saveButton.onClick.AddListener(SaveColor);
    }

    private void SaveColor()
    {
        loadManager.SaveData();
    }
}
