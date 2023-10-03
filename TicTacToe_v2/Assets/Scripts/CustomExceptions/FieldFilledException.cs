using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFilledException : Exception
{
    public FieldFilledException() { }
    public FieldFilledException(string message) : base(message) { }
}
