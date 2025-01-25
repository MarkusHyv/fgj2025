using System;
using UnityEngine;

public class BubbleBurster : MonoBehaviour
{
    public Action<BubbleType> OnBubbleBurst;

    private void OnCollisionEnter(Collision other)
    {
        UnityEngine.Debug.Log("Collision detected");
        if (other.gameObject.TryGetComponent(out Bubble bubble))
        {
            OnBubbleBurst?.Invoke(bubble.BubbleType);
            Destroy(other.gameObject);
        }
    }

}
