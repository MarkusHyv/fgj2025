using TMPro;
using UnityEngine;

namespace ProtoToolkit.Scripts.UI.Common
{
    [RequireComponent(typeof(TMP_Text))]
    public class UIData_NameText : UIDataBinder<IUIData>
    {
        [SerializeField] private TMP_Text _text;
    
        public override void OnValidate()
        {
            base.OnValidate();
            _text = GetComponent<TMP_Text>();
        }
    
        public override void ApplyData(IUIData data)
        {
            _text.text = data.Name;
        }
    }
}
