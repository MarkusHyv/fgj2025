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
    [SerializeField] private SpriteRenderer _baseRenderer;
    [SerializeField] private SpriteRenderer _faceRenderer;
    [SerializeField] private SpriteRenderer _shineRenderer;
    [SerializeField] private SpriteRenderer _accessoryRenderer;

    private static bool _resourcesLoaded = false;
    private const string _faceResourcesPath = "BubbleFaces";
    private const string _accessoryResourcesPath = "BubbleAccessories";

    private static Sprite[] _faceSprites;
    private static Sprite[] _accessorySprites;


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

    private void LoadResources()
    {
        if(_resourcesLoaded) return;

        _faceSprites = Resources.LoadAll<Sprite>(_faceResourcesPath);
        _accessorySprites = Resources.LoadAll<Sprite>(_accessoryResourcesPath);
        _resourcesLoaded = true;
    }

    private void SetVisuals(BubbleType spawnBubbleType)
    {
        LoadResources();

        if (spawnBubbleType == BubbleType.PlusHealthBubble)
            return;

        _faceRenderer.sprite = _faceSprites[UnityEngine.Random.Range(0, _faceSprites.Length)];
        _accessoryRenderer.sprite = _accessorySprites[UnityEngine.Random.Range(0, _accessorySprites.Length)];

        RandomizeColor();

        if (spawnBubbleType == BubbleType.EvilBubble)
            SetEvilVisuals();
    }

    private void SetEvilVisuals()
    {
        _baseRenderer.sprite = _bubbleVisualsProvider.GetBubbleVisuals(BubbleType.EvilBubble).BaseSprites[0];
    }

    private void RandomizeColor()
    {
        var color = Random.ColorHSV(
            0f, 1f, 
            0.15f, 0.3f,
            0.7f, 1f);
        _baseRenderer.color = color;
        _shineRenderer.color = new(color.r, color.g, color.b, _shineRenderer.color.a);
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
