using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private Vector2 rawInputMovement;

    private static Input _instance;

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

    public void MoveInput(InputAction.CallbackContext context)
    {
        rawInputMovement = context.ReadValue<Vector2>();
    }

    public Vector2 GetRawMovement() => rawInputMovement;
}
