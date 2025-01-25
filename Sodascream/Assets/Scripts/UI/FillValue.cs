using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillValue : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void OnValidate()
        {
            _image = GetComponent<Image>();
        }

        public void SetFillValue(float value)
        {
            _image.fillAmount = Mathf.Clamp01(value);
        }
    }
}
