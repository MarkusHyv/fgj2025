using System.Collections.Generic;
using UnityEngine;

namespace ProtoToolkit.Scripts.UI.Common
{
    public class CountableIconGroup : MonoBehaviour
    {
        [SerializeField]
        private List<CountableIcon> _icons;
        [SerializeField]
        private CountableIcon _prefab;
        [SerializeField]
        private RectTransform _root;
        [SerializeField, Range(0, 10)]
        private int _maxValue = 3;
        [SerializeField]
        private int _currentValue;

        public void OnValidate()
        {
            if (!Application.isPlaying) return;
            if (_root == null || _icons == null) return;
            SetMaxValue(_maxValue);
            SetValue(_currentValue);
        }

        public void OnEnable()
        {
            if (_root == null || _icons == null) return;
            SetMaxValue(_maxValue);
            SetValue(_currentValue);
        }

        public void SetValue(int value)
        {
            _currentValue = value;
            for (var i = 0; i < _icons.Count; i++)
            {
                var currentIndex = i + 1;
                _icons[i].SetEnabled(currentIndex <= value && i < _maxValue);
            }
        }

        public void SetMaxValue(int value)
        {
            _maxValue = value;
            while (_icons.Count < value)
            {

                _icons.Add(GetItem());
            }

            for (var i = 0; i < _icons.Count; i++)
            {
                _icons[i].gameObject.SetActive(i < _maxValue);
            }

        }

        private CountableIcon GetItem()
        {
            var newObj = Instantiate(_prefab, _root);
            return newObj;
        }
        
    }
}
