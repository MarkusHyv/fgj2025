using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public BubbleType BubbleType;
    bool _isInitialized = false;
    private float _speed;
    private Vector3 _direction;
    private int _scoreIncrease;
    private float _elapsedLifeTime = 0f;
    private float _lifeTimeMax;
    private int _lifeIncreaseOnBurst;
    private int _lifeDecreaseOnHit;

    internal int GetLifeIncrease()
    {
        return _lifeIncreaseOnBurst;
    }

    public int GetLifeDecrease()
    {
        return _lifeDecreaseOnHit;
    }

    internal int GetScoreIncrease()
    {
        return _scoreIncrease;
    }

    internal void Initialize(BubbleType spawnBubbleType, float speed, float sizeMultiplier, Vector3 randomDirection, int scoreIncrease, float bubbleLifeTimeInSeconds, int lifeIncreaseOnBurst, int lifeDecreaseOnHit)
    {
        transform.localScale = new Vector3(
     transform.localScale.x * sizeMultiplier,
     transform.localScale.y * sizeMultiplier,
     transform.localScale.z * sizeMultiplier
     );
        _speed = speed;
        _isInitialized = true;
        _direction = randomDirection;
        _scoreIncrease = scoreIncrease;
        _lifeTimeMax = bubbleLifeTimeInSeconds;
        _lifeIncreaseOnBurst = lifeIncreaseOnBurst;
        _lifeDecreaseOnHit = lifeDecreaseOnHit;
    }

    private void Update()
    {
        if (_isInitialized)
        {
            var targetPosition = transform.position + _direction * _speed * Time.deltaTime;
            transform.position = targetPosition;
            _elapsedLifeTime += Time.deltaTime;
            if (_elapsedLifeTime > _lifeTimeMax)
            {
                Destroy(gameObject);
            }
        }
    }
}
