using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ProtoToolkit.Scripts.UI.Graphics
{
    public class GraphicsManager : Singleton<GraphicsManager>
    {
        public static UnityEvent<Vector2Int> ScreenResolutionChanged = new UnityEvent<Vector2Int>();
        public static Vector2Int CurrentResolution { get; private set; }
        private float _timer;
    
        public const int c_targetHeight = 540;
        public const int c_targetFrameRate = 60;
        private bool _isPotato = false;
        [SerializeField] private UniversalRenderPipelineAsset _potatoAsset;
        [SerializeField] private UniversalRenderPipelineAsset _ultraAsset;
        [SerializeField] private UniversalRenderPipelineAsset _renderPipelineAsset;
    
        public void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            var d= !SystemInfo.graphicsMultiThreaded
                   || SystemInfo.processorCount <= 3
                   || SystemInfo.graphicsMemorySize < 2000;
            _isPotato = PlayerPrefs.GetInt("IsPotato", d ? 1 : 0) == 1;
        }

        public void OnEnable()
        {
            CurrentResolution = new Vector2Int(Screen.width, Screen.height);
            ScreenResolutionChanged.AddListener(LimitResolution);
            ScreenResolutionChanged.Invoke(CurrentResolution);
        }

        public void OnDisable()
        {
            ScreenResolutionChanged.RemoveListener(LimitResolution);
        }

        public void SetIsPotato(bool isPotato)
        {
            _isPotato = isPotato;
            PlayerPrefs.SetInt("IsPotato", _isPotato ? 1 : 0);
            PlayerPrefs.Save();
            CurrentResolution = new Vector2Int(Screen.width, Screen.height);
            ScreenResolutionChanged.Invoke(CurrentResolution);
        }

        public void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer >= 0f)
                return;
        
            if (CurrentResolution.y != Screen.height || CurrentResolution.x != Screen.width)
            {
                CurrentResolution = new Vector2Int(Screen.width, Screen.height);
                ScreenResolutionChanged.Invoke(CurrentResolution);
            }
            _timer = 1f;
        }
        public void LimitFrameRate(int frameRate)
        {
            Application.targetFrameRate = frameRate;
        }

        // We compare screen res to 1080 and adjust it to match somewhat that in height ratio.
        // Most likely phones would be rendering at higher resolutions, such as the newer phones
        // but I think 1080p should be a pretty solid standard.
        public void LimitResolution(Vector2Int resolution)
        {
            if (resolution.y <= c_targetHeight)
                return;
            
            var mult = c_targetHeight / (float)CurrentResolution.y;
            _renderPipelineAsset = _isPotato ? _potatoAsset : _ultraAsset;
            var upscaling = FSRUtils.IsSupported() && !_isPotato ? UpscalingFilterSelection.FSR : UpscalingFilterSelection.Linear;
            _renderPipelineAsset.upscalingFilter = upscaling;
            _renderPipelineAsset.renderScale = Mathf.Min(mult, 1);
            LimitFrameRate(c_targetFrameRate);
            var settingText = _isPotato ? "Potato" : "Ultra";
            Debug.LogWarning($"Graphics quality changed: {settingText}");
            QualitySettings.SetQualityLevel(_isPotato ? 1 : 0);
        }
    }
}
