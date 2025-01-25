using System;

public interface IInputReader
{
    event Action<float> OnTurnInput;
}