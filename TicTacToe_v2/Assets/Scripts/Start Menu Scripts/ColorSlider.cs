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

        SetInitialValue(colorSlider);

        colorComponentText.text = colorSlider.value.ToString();

        colorSlider.onValueChanged.AddListener(ChangeColor);
    }

    private void SetInitialValue(Slider colorSlider)
    {
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
    }

    private void ChangeColor(float value)
    {
        colorComponentText.text = value.ToString();

        float normalziedValue = value / MAX_COLOR_VALUE;

        SetNewColor(normalziedValue);
    }

    private void SetNewColor(float normalizedValue)
    {
        Color newColor = loadManager.CurrentColor;

        switch (colorComponent)
        {
            case ColorComponent.R:
                newColor.r = normalizedValue;
                break;
            case ColorComponent.G:
                newColor.g = normalizedValue;
                break;
            case ColorComponent.B:
                newColor.b = normalizedValue;
                break;
        }

        loadManager.CurrentColor = newColor;
    }
}
