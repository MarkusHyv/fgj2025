using System;
using UnityEngine;
public interface IInputReader
{
    event Action<float> OnTurnInput;
}

public interface IGyroInputReader
{
    public bool CanProvideInput();
    public float GetGyroTurnDirection();
}