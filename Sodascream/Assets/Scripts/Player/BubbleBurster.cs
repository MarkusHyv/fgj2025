using System;
using UnityEngine;

public class BubbleBurster : MonoBehaviour
{
    public Action<BubbleType> OnBubbleBurst;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Bubble bubble))
        {
            OnBubbleBurst?.Invoke(bubble.BubbleType);
            Destroy(other.gameObject);
        }
    }

}
