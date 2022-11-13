using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionManager : MonoBehaviour
{
    private static ObstructionManager _instance;
    public readonly List<Obstruction> remaining = new ();

    public static ObstructionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObstructionManager>();
            }

            return _instance;
        }
    }
    
    private void RemoveObstruction(Obstruction destroyedOb)
    {
        if (destroyedOb && !destroyedOb.destroyed) return;
        remaining.Remove(destroyedOb);
        if (remaining.Count <= 0)
        {
            GameEvents.Instance.lastObsctructionDestroyed.Invoke();
        }
    }
}
