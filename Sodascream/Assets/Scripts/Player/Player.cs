using ProtoToolkit.Scripts.DTO;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerCollisionTracker _collisionTracker;
    [SerializeField] private BubbleBurster _bubbleBurster;
    [SerializeField] private DtoInt _dtoScore;
    [SerializeField] private DtoInt _dtoHealth;

    private int _MaxHealth = 3;
    private int _currentHealth;
    private int _score;

    private void Start()
    {
        _currentHealth = _MaxHealth;
        _dtoHealth.OnValueChanged?.Invoke(_currentHealth);
        _collisionTracker = new PlayerCollisionTracker(_bubbleBurster, this);
    }

    public void IncreaseScore(BubbleType type, int score)
    {
        _score += score;
        _dtoScore.OnValueChanged?.Invoke(_score);
    }

    public void IncreaseHealth(int byValue)
    {
        _currentHealth += byValue;
        if (_currentHealth > _MaxHealth)
        {
            _currentHealth = _MaxHealth;
        }


        _dtoHealth.OnValueChanged?.Invoke(_currentHealth);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Bubble bubble))
        {
            IncreaseHealth(bubble.GetLifeDecrease());
            Destroy(other.gameObject);
        }
    }
}
