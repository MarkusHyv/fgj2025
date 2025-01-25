using ProtoToolkit.Scripts.DTO;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerCollisionTracker _collisionTracker;
    [SerializeField] private BubbleBurster _bubbleBurster;
    [SerializeField] private DtoInt _dtoScore;
    private int _score;

    private void Start()
    {
        _collisionTracker = new PlayerCollisionTracker(_bubbleBurster, this);
    }

    public void IncreaseScore(BubbleType type, int score)
    {
        _score += score;
        _dtoScore.OnValueChanged?.Invoke(_score);
    }
}
