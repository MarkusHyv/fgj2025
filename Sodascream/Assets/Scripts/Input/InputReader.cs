using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, PlayerControls.IGameplayActions, IInputReader
{
    public event Action<float> OnTurnInput;
    PlayerControls _playerControls;
    void Start()
    {
        _playerControls = new PlayerControls();
        _playerControls.Gameplay.SetCallbacks(this);
        _playerControls.Gameplay.Enable();
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnTurnInput?.Invoke(context.ReadValue<float>());
        }
    }

    private void OnDisable()
    {
        _playerControls.Gameplay.Disable();
    }
}
