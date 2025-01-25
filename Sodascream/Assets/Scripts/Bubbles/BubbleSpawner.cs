using System;
using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private BubbleConfigScriptableObject _bubbleConfigProvider;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _maximumSpawnDistanceFromPlayer;
    [SerializeField] private float _minimumSpawnDistanceFromPlayer;
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
        int spawnBubbleTypeIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(BubbleType)).Length);
        BubbleType spawnBubbleType = (BubbleType)spawnBubbleTypeIndex;
        var bubbleConfig = _bubbleConfigProvider.GetBubbleConfig(spawnBubbleType);
        Bubble bubble = Instantiate<Bubble>(bubbleConfig.BubblePrefab, randomPositionFromPlayer, Quaternion.identity);
        var randomSizeMultiplier = UnityEngine.Random.Range(bubbleConfig.SizeMultiplier - bubbleConfig.SizeRandomVariance, bubbleConfig.SizeMultiplier);
        Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f)).normalized;

        bubble.Initialize(
            spawnBubbleType,
            bubbleConfig.SpeedMultiplier * _bubbleConfigProvider.BaseBubbleSpeed,
            randomSizeMultiplier,
            randomDirection,
            bubbleConfig.ScoreValue
            );

    }
}
