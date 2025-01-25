using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public BubbleType BubbleType;
    bool _isInitialized = false;
    private float _speed;
    private Vector3 _direction;

    internal void Initialize(BubbleType spawnBubbleType, float speed, float sizeMultiplier, Vector3 randomDirection)
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
