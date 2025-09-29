using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInput playerInputAction;

    public event EventHandler OnInteractPress;
    public event EventHandler OnThrowPress;
    public event EventHandler OnGrabPress;



    private void Awake()
    {
        Instance = this;

        playerInputAction = new PlayerInput();

        playerInputAction.Enable();


    }

    private void Start()
    {
        playerInputAction.Player.Throw.performed += Throw_performed;
        playerInputAction.Player.Grab.performed += Grab_performed;
        playerInputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractPress?.Invoke(this, EventArgs.Empty);
    }

    private void Grab_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGrabPress?.Invoke(this, EventArgs.Empty);
    }

    private void Throw_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnThrowPress?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
    public Vector2 GetLookVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Look.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
