using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputProcessor 
{
    public bool GetKeyDown(KeyCode key);

    public (int, int) GetClickedButton();

    public void Reset();
}
