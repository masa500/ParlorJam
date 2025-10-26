using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, GameInputs.IGameplayActions
{
    public event UnityAction clickPerformedEvent;
    public event UnityAction clickCanceledEvent;

    public event UnityAction<Vector2> mousePositionEvent;

    private GameInputs _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInputs();
            _gameInput.Gameplay.SetCallbacks(this);
        }

        EnableControls();
    }

    private void OnDisable()
    {
        DisableControls();
    }

    public void DisableControls()
    {
        _gameInput.Gameplay.Disable();
    }

    public void EnableControls()
    {
        _gameInput.Gameplay.Enable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            clickPerformedEvent?.Invoke();
            Debug.Log("Click performed");
        }
        else if (context.canceled)
        {
            clickCanceledEvent?.Invoke();
            Debug.Log("Click canceled");
        }
    }
    
    public void OnPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mousePositionEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
