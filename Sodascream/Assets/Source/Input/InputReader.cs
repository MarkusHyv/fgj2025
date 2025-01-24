using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, PlayerControls.IGameplayActions
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerControls playerControls = new PlayerControls();
        playerControls.Gameplay.SetCallbacks(this);
        playerControls.Gameplay.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTurn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }
}
