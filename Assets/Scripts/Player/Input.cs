using System;
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

    private void Start()
    {
        Time.timeScale = 1;
        playerInput.SwitchCurrentActionMap(InputMap.Player.ToString());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        GameEvents.Instance.togglePause.AddListener(PauseSwitchMap);
        GameEvents.Instance.lastObsctructionDestroyed.AddListener(OpenUI);
    }

    private void OnDisable()
    {
        if (!GameEvents.Instance) return;
        GameEvents.Instance.togglePause.RemoveListener(PauseSwitchMap);
        GameEvents.Instance.lastObsctructionDestroyed.RemoveListener(OpenUI);
    }

    private void OpenUI()
    {
        playerInput.SwitchCurrentActionMap(InputMap.UI.ToString());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        
        Cursor.visible = paused;
        Cursor.lockState = paused ? CursorLockMode.None :  CursorLockMode.Locked;
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

