using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    private const string COLOR_FILE_PATH = "./colors.json";

    private Color currentColor;

    public Color CurrentColor
    {
        get => currentColor;
        set
        {
            currentColor = value;
            ColorChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler ColorChanged;

    // Start is called before the first frame update
    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (!File.Exists(COLOR_FILE_PATH))
        {
            using (var fs = File.Create(COLOR_FILE_PATH))
            {
                var basicColorJson = JsonUtility.ToJson(new Color(255, 255, 255));
                byte[] basicColorJsonBytes = Encoding.UTF8.GetBytes(basicColorJson);
                fs.Write(basicColorJsonBytes);
            }
        }

        var json = File.ReadAllText(COLOR_FILE_PATH);
        CurrentColor = JsonUtility.FromJson<Color>(json);
    }

    public void SaveData()
    {
        if (File.Exists(COLOR_FILE_PATH))
        {
            var json = JsonUtility.ToJson(CurrentColor);
            File.WriteAllText(COLOR_FILE_PATH, json);
        }
    }
}
