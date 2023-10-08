using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableElementException : Exception
{
    public PlayableElementException() : base() { }
    public PlayableElementException(string message) : base(message) { }
}
