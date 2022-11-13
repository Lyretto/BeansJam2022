using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    private static GameEvents _instance;

    public static GameEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameEvents>();
            }

            return _instance;
        }
    }

    public UnityEvent tiredTimerExpired;

    public UnityEvent lastObsctructionDestroyed;

    public UnityEvent<bool> togglePause;

    public UnityEvent<Interactables> contactInteractable;

    public UnityEvent interactInput;

    public UnityEvent<PlayerState> transforming;

    public UnityEvent calm;

    public UnityEvent<Obstruction> obstructionHit;

    public UnityEvent rage;

    public UnityEvent rageDown;
}
