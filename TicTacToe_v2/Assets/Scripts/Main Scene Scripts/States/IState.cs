using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter(params object[] parameters);

    public void Exit();

    public void Update();

    public void Render();
}
