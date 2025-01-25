using System;
using UnityEngine;

public class PlayerCollisionTracker : MonoBehaviour
{
    [SerializeField] private BubbleBurster _bubbleBurster;

    private void Start()
    {
        if (_bubbleBurster == null)
        {
            throw new System.Exception($"BubbleBurster not found in {transform.name}");
        }
        _bubbleBurster.OnBubbleBurst += OnBubbleBurst;
    }

    private void OnBubbleBurst(BubbleType type)
    {
        Debug.Log("Player has burst a bubble of type: " + type);
    }

    private void OnDestroy()
    {
        _bubbleBurster.OnBubbleBurst -= OnBubbleBurst;
    }
}
