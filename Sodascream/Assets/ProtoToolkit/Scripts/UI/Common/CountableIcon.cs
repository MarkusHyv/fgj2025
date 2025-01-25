using UnityEngine;
using UnityEngine.UI;

namespace ProtoToolkit.Scripts.UI.Common
{
    public class CountableIcon : MonoBehaviour, IToggleable
    {
        [SerializeField] public Sprite _enabledSprite;
        [SerializeField] public Sprite _disabledSprite;
        [SerializeField] public Image _image;
        
        public bool Enabled { get; private set; }
        public void SetEnabled(bool value)
        {
            Enabled = value;
            _image.sprite = value ? _enabledSprite : _disabledSprite;
        }
    }
}
