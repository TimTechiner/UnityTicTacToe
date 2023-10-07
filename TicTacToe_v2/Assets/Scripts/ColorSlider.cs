using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour
{
    [SerializeField]
    private LoadManager loadManager;

    [SerializeField]
    private ColorComponent colorComponent;

    [SerializeField]
    private TextMeshProUGUI colorComponentText;

    private const int MAX_COLOR_VALUE = 255;

    // Start is called before the first frame update
    private void Start()
    {
        Slider colorSlider = GetComponent<Slider>();

        switch (colorComponent)
        {
            case ColorComponent.R:
                colorSlider.value = loadManager.CurrentColor.r * MAX_COLOR_VALUE;
                break;
            case ColorComponent.G:
                colorSlider.value = loadManager.CurrentColor.g * MAX_COLOR_VALUE;
                break;
            case ColorComponent.B:
                colorSlider.value = loadManager.CurrentColor.b * MAX_COLOR_VALUE;
                break;
        }

        colorComponentText.text = colorSlider.value.ToString();

        colorSlider.onValueChanged.AddListener(ChangeColor);
    }

    private void ChangeColor(float value)
    {
        colorComponentText.text = value.ToString();

        value = value / MAX_COLOR_VALUE;

        switch (colorComponent)
        {
            case ColorComponent.R:
                loadManager.CurrentColor = new Color(
                    value, 
                    loadManager.CurrentColor.g,
                    loadManager.CurrentColor.b);
                break;
            case ColorComponent.G:
                loadManager.CurrentColor = new Color(
                    loadManager.CurrentColor.r,
                    value,
                    loadManager.CurrentColor.b);
                break;
            case ColorComponent.B:
                loadManager.CurrentColor = new Color(
                    loadManager.CurrentColor.r,
                    loadManager.CurrentColor.g,
                    value);
                break;
        }
    }
}
