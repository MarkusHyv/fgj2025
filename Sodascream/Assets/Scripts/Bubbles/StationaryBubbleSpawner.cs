using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryBubbleSpawner : MonoBehaviour, IStoppableElement
{

    [SerializeField] private BubbleConfigScriptableObject _bubbleConfigProvider;
    [SerializeField] private float _spawnIntervalAtStartInSeconds;
    [SerializeField] private float _spawnIntervalMinimumInSeconds;
    [SerializeField] private float _timeToReachMinimumSpawnIntervalInSeconds;
    [SerializeField] private List<BubbleType> _spawnsBubbleTypes;
    [SerializeField] private float _maximumSpawnDistanceFromOrigin;

    private float _currentSpawnInterval;
    private bool _started;

    private void Start()
    {
        _currentSpawnInterval = _spawnIntervalAtStartInSeconds;

    }

    private IEnumerator SpawnBubbleCoroutine()
    {
        while (_started)
        {
            SpawnRandomBubble();
            yield return new WaitForSeconds(_currentSpawnInterval);
        }
    }

    private void SpawnRandomBubble()
    {
        int spawnBubbleTypeIndex = UnityEngine.Random.Range(0, _spawnsBubbleTypes.Count);
        BubbleType spawnBubbleType = _spawnsBubbleTypes[spawnBubbleTypeIndex];
        var bubbleConfig = _bubbleConfigProvider.GetBubbleConfig(spawnBubbleType);

        var randomPosition = new Vector3(
            UnityEngine.Random.Range(-_maximumSpawnDistanceFromOrigin, _maximumSpawnDistanceFromOrigin),
            0f,
            UnityEngine.Random.Range(-_maximumSpawnDistanceFromOrigin, _maximumSpawnDistanceFromOrigin)
        );

        Bubble bubble = Instantiate<Bubble>(_bubbleConfigProvider.GetBubbleConfig(spawnBubbleType).Prefab, this.transform.position + randomPosition, Quaternion.identity, this.transform);
        var randomSizeMultiplier = UnityEngine.Random.Range(bubbleConfig.SizeMultiplier - bubbleConfig.SizeRandomVariance, bubbleConfig.SizeMultiplier);
        Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f)).normalized;

        bubble.Initialize(
            spawnBubbleType: spawnBubbleType,
            speed: bubbleConfig.SpeedMultiplier * _bubbleConfigProvider.BaseBubbleSpeed,
            sizeMultiplier: randomSizeMultiplier,
            randomDirection: randomDirection,
            scoreIncrease: bubbleConfig.ScoreValue,
            bubbleLifeTimeInSeconds: _bubbleConfigProvider.BubbleLifeTimeInSeconds,
            lifeIncreaseOnBurst: bubbleConfig.BubbleLifeIncrease,
            lifeDecreaseOnHit: bubbleConfig.BubbleLifeDecrease
            );
    }

    private void Update()
    {
        _currentSpawnInterval = Mathf.Lerp(_spawnIntervalAtStartInSeconds, _spawnIntervalMinimumInSeconds, Time.time / _timeToReachMinimumSpawnIntervalInSeconds);
    }

    public void StartAction()
    {
        Debug.Log("StartAction called in stationary spawner");
        _started = true;
        StartCoroutine(SpawnBubbleCoroutine());
    }

    public void StopAction()
    {
        _started = false;
    }
}
