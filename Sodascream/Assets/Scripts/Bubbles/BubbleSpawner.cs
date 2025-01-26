using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private BubbleConfigScriptableObject _bubbleConfigProvider;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _maximumSpawnDistanceFromPlayer;
    [SerializeField] private float _minimumSpawnDistanceFromPlayer;
    [SerializeField] private List<BubbleType> _spawnsBubbleTypes;
    private Player _player;
    private void Start()
    {
        _player = FindFirstObjectByType<Player>();
        StartCoroutine(SpawnBubbleCoroutine());
    }

    private IEnumerator SpawnBubbleCoroutine()
    {
        while (true)
        {
            SpawnRandomBubble();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnRandomBubble()
    {
        var randomPositionFromPlayer = new Vector3(
            _player.transform.position.x + UnityEngine.Random.Range(_minimumSpawnDistanceFromPlayer, _maximumSpawnDistanceFromPlayer),
            _player.transform.position.y,
            _player.transform.position.z + UnityEngine.Random.Range(_minimumSpawnDistanceFromPlayer, _maximumSpawnDistanceFromPlayer)
        );

        int spawnBubbleTypeIndex = UnityEngine.Random.Range(0, _spawnsBubbleTypes.Count);
        BubbleType spawnBubbleType = _spawnsBubbleTypes[spawnBubbleTypeIndex];

        var bubbleConfig = _bubbleConfigProvider.GetBubbleConfig(spawnBubbleType);
        Bubble bubble = Instantiate<Bubble>(_bubbleConfigProvider.BaseBubblePrefab, randomPositionFromPlayer, Quaternion.identity);
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
}
