using UnityEngine;
using UnityEngine.UI;

namespace ProtoToolkit.Scripts.UI.Common
{
    [RequireComponent(typeof(Image))]
    public class UIData_IconImage : UIDataBinder<IUIData>
    {
        [SerializeField]
        private Image _image;
        
        public override void OnValidate()
        {
            base.OnValidate();
            _image = GetComponent<Image>();
        }
        
        public override void ApplyData(IUIData data)
        {
            _image.sprite = (data as IUIDataIcon)?.Icon;
        }
    }
}
