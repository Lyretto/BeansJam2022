using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private Vector2 rawInputMovement;

    private static Input _instance;
    private PlayerInput playerInput;

    public static Input Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Input>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        rawInputMovement = context.ReadValue<Vector2>();
    }

    public Vector2 GetRawMovement() => rawInputMovement;

    public void OpenPause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        playerInput.SwitchCurrentActionMap(InputMap.UI.ToString());
        GameEvents.Instance.togglePause.Invoke(true);
    }
    
    public void ClosePause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        playerInput.SwitchCurrentActionMap(InputMap.Player.ToString());
        GameEvents.Instance.togglePause.Invoke(false);
    }
}

public enum InputMap
{
    Player, UI
}

