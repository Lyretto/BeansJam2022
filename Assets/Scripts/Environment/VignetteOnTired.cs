using System;
using UnityEngine;
using UnityEngine.Rendering;

public class VignetteOnTired : MonoBehaviour
{
    private Volume volume;
    private void Awake()
    {
        volume = GetComponent<Volume>();
    }
    
}
