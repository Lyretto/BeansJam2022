using System.Collections;
using System.Collections.Generic;
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
}
