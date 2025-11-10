using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions inputActions;

    public event EventHandler OnJump;
    public event EventHandler OnPaused;

    private void Awake()
    {
        Instance = this;
        inputActions = new PlayerInputActions();
        inputActions.Enable();

        inputActions.Bubble.Jump.performed += Jump_performed;
        inputActions.Bubble.Pause.performed += OnPause_performed;
  
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }
    private void OnPause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPaused?.Invoke(this, EventArgs.Empty);
    }
     

   
}
