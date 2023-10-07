using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentColorPanel : MonoBehaviour
{
    [SerializeField]
    private LoadManager loadManager;

    private Image panelImage;

    // Start is called before the first frame update
    private void Start()
    {
        panelImage = GetComponent<Image>();
        panelImage.color = loadManager.CurrentColor;

        loadManager.ColorChanged += LoadManager_ColorChanged;
    }

    private void LoadManager_ColorChanged(object sender, System.EventArgs e)
    {
        panelImage.color = loadManager.CurrentColor;
    }
}
