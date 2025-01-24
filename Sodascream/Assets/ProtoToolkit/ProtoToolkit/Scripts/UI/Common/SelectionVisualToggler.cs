using System.Collections.Generic;
using UnityEngine;

namespace ProtoToolkit.Scripts.UI.Common
{
    public class SelectionVisualToggler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _gameObjects; 
        
        public void OnSelectionChanged(bool value)
        {
            foreach (var go in _gameObjects)
            {
                go.GameObjectSetActive(value);
            }
        }
    }
}
