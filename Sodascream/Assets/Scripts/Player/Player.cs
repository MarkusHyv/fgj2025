using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerCollisionTracker _collisionTracker;
    [SerializeField] private BubbleBurster _bubbleBurster;
    private long _score;

    private void Start()
    {
        _collisionTracker = new PlayerCollisionTracker(_bubbleBurster, this);
    }

    public void IncreaseScore(BubbleType type)
    {
        //TODO make dynamic based on bubble type?
        _score += 1;
    }
}
