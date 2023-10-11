using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldInvalidException : Exception
{
    public FieldInvalidException() { }
    public FieldInvalidException(string message) : base(message) { }
}
