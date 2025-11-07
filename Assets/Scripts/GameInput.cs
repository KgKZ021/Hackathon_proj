using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public event EventHandler OnJump;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();

        inputActions.Bubble.Jump.performed += Jump_performed;
  
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(this,EventArgs.Empty);
    }

   
}
