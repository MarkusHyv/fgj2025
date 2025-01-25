using System;
using UnityEngine;

public class BubbleBurster : MonoBehaviour
{
    public Action<BubbleType, int> OnBubbleBurst;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Bubble bubble))
        {
            OnBubbleBurst?.Invoke(bubble.BubbleType, bubble.GetScoreIncrease());
            Destroy(other.gameObject);
        }
    }

}
