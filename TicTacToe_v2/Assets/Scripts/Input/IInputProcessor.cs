using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputProcessor 
{
    public KeyCode GetKey();

    public (int, int) GetClickedButton();

    public void Reset();
}
