using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private BubbleType _bubbleType;
    bool _isInitialized = false;
    private float _speed;
    private Vector3 _direction;
    private int _scoreIncrease;
    private float _elapsedLifeTime = 0f;
    private float _lifeTimeMax;
    private int _lifeIncreaseOnBurst;
    private int _lifeDecreaseOnHit;
    [SerializeField] private BubbleVisualsScriptableObject _bubbleVisualsProvider;
    [SerializeField] private MeshRenderer _faceRenderer;
    [SerializeField] private MeshRenderer _baseRenderer;

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
        _bubbleType = spawnBubbleType;
        _speed = speed;
        _isInitialized = true;
        _direction = randomDirection;
        _scoreIncrease = scoreIncrease;
        _lifeTimeMax = bubbleLifeTimeInSeconds;
        _lifeIncreaseOnBurst = lifeIncreaseOnBurst;
        _lifeDecreaseOnHit = lifeDecreaseOnHit;
        SetVisuals(spawnBubbleType);
    }

    private void SetVisuals(BubbleType spawnBubbleType)
    {
        var tempFaceMaterial = new Material(_faceRenderer.material);
        var tempBaseMaterial = new Material(_bubbleVisualsProvider.GetBubbleVisuals(spawnBubbleType).BaseMaterialForType);
        var visualsConfig = _bubbleVisualsProvider.GetBubbleVisuals(spawnBubbleType);
        var faceSprite = visualsConfig.FaceSprites[UnityEngine.Random.Range(0, visualsConfig.FaceSprites.Length)];
        var baseSprite = visualsConfig.BaseSprites[UnityEngine.Random.Range(0, visualsConfig.BaseSprites.Length)];
        //        var accessorySprite = visualsConfig.AccessorySprites[UnityEngine.Random.Range(0, visualsConfig.AccessorySprites.Length)];
        tempFaceMaterial.mainTexture = faceSprite.texture;
        tempBaseMaterial.mainTexture = baseSprite.texture;
        _faceRenderer.material = tempFaceMaterial;
        _baseRenderer.material = tempBaseMaterial;
        //TODO use accessory sprite too
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

    internal BubbleType GetBubbleType() => _bubbleType;
}
