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

    private void OnEnable()
    {
        GameEvents.Instance.togglePause.AddListener(PauseSwitchMap);
    }

    private void OnDisable()
    {
        if (!GameEvents.Instance) return;
        GameEvents.Instance.togglePause.RemoveListener(PauseSwitchMap);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        rawInputMovement = context.ReadValue<Vector2>();
    }

    public Vector2 GetRawMovement() => rawInputMovement;

    public void OpenPause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        GameEvents.Instance.togglePause.Invoke(true);
    }
    
    public void ClosePause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        GameEvents.Instance.togglePause.Invoke(false);
    }

    private void PauseSwitchMap(bool paused)
    {
        playerInput.SwitchCurrentActionMap(paused ? InputMap.UI.ToString() : InputMap.Player.ToString());
    }
    
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        GameEvents.Instance.interactInput.Invoke();
    }
}

public enum InputMap
{
    Player, UI
}

