using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    public Element Element { get; set; }
    protected Player() { }
    public abstract ICommand MakeTurn(GameField field);
}
