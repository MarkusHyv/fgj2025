using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public BubbleType BubbleType;
    bool _isInitialized = false;
    private float _speed;
    private Vector3 _direction;
    private int _scoreIncrease;

    internal int GetScoreIncrease()
    {
        return _scoreIncrease;
    }

    internal void Initialize(BubbleType spawnBubbleType, float speed, float sizeMultiplier, Vector3 randomDirection, int scoreIncrease)
    {
        //scale the gameObject
        transform.localScale = new Vector3(
            transform.localScale.x * sizeMultiplier,
            transform.localScale.y * sizeMultiplier,
            transform.localScale.z * sizeMultiplier
            );
        _speed = speed;
        _isInitialized = true;
        _direction = randomDirection;
        _scoreIncrease = scoreIncrease;
    }

    private void Update()
    {
        if (_isInitialized)
        {
            var targetPosition = transform.position + _direction * _speed * Time.deltaTime;
            transform.position = targetPosition;
        }
    }
}
