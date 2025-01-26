using System;
using UnityEngine;

public class BubbleBurster : MonoBehaviour
{
    public Action<BubbleType, int, int> OnBubbleBurst;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Bubble bubble))
        {
            OnBubbleBurst?.Invoke(bubble.GetBubbleType(), bubble.GetScoreIncrease(), bubble.GetLifeIncrease());
            Destroy(other.gameObject);
        }
    }

}
