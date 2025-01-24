using UnityEngine;

namespace ProtoToolkit.Scripts.UI.State
{
    [CreateAssetMenu(fileName = "StateDefinition", menuName = "UIState/StateDefinition")]
    public class UIStateDefinition : ScriptableObject
    {
        [SerializeField] private GameObject _statePrefab;
        public GameObject StatePrefab => _statePrefab;
        [SerializeField] public E_UIStateType _stateType;
        public E_UIStateType StateType => _stateType;
    }
}
