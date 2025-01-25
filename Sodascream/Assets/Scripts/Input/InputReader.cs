using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, PlayerControls.IGameplayActions, IInputReader
{
    public event Action<float> OnTurnInput;
    void Start()
    {
        PlayerControls playerControls = new PlayerControls();
        playerControls.Gameplay.SetCallbacks(this);
        playerControls.Gameplay.Enable();
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnTurnInput?.Invoke(context.ReadValue<float>());
        }
    }
}
