using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProtoToolkit.Scripts.UI.Common
{
    public class ListButtons : MonoBehaviour
    {
        [SerializeField] private ListButton _prefab;
        [SerializeField] private RectTransform _root;
        [SerializeField] private List<ListButton> _buttons;
        public UnityEvent<int> OnSelectionChanged;
        public UnityEvent<RectTransform> SelectedRectTransformChanged = new UnityEvent<RectTransform>();
        [SerializeField]
        private int _currentSelection = -1;

        public void OnValidate()
        {
            _root = GetComponent<RectTransform>();
        }

        public void OnDisable()
        {
            _currentSelection = -1;
        }

        public void Reset()
        {
            _root.ClearChildren();
            _buttons.Clear();
        }

        public void UpdateData<T>(T[] objects) where T : IUIData
        {
            for (var i = 0; i < objects.Length; i++)
            {
                var button = GetButton(i);
                button.gameObject.GameObjectSetActive(true);
                button.SetData(objects[i]);
                button.SetSelected(_currentSelection == i);
            }

            for (var i = objects.Length; i < _buttons.Count; i++)
            {
                var btn = GetButton(i);
                btn.gameObject.GameObjectSetActive(false);
            }
        }

        private ListButton GetButton(int index)
        {
            if (_buttons.Count <= index)
            {
                var btn = Instantiate(_prefab, _root);
                btn.transform.localScale = Vector3.one;
                btn.OnClick.AddListener(ButtonPressed);
                _buttons.Add(btn);
                return btn;
            }
            else
            {
                return _buttons[index];
            }
        }

        private void ButtonPressed(int index)
        {
            UpdateCurrentSelection(index);
        }

        public RectTransform GetCurrentSelectedObject() => _buttons[_currentSelection]?.transform as RectTransform;

        public void UpdateCurrentSelection(int index)
        {
            _currentSelection = index;
            OnSelectionChanged.Invoke(_currentSelection);
            for (var i = 0; i < _buttons.Count; ++i)
            {
                var selected = i == _currentSelection;
                _buttons[i].SetSelected(selected);
                if (selected)
                {
                    SelectedRectTransformChanged.Invoke(_buttons[i].transform as RectTransform);
                }
            }
        }
    }
    
}
