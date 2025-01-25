using UnityEngine;

namespace ProtoToolkit.Scripts.UI.State
{
    [CreateAssetMenu(fileName = "UIGameplayStateManagerCommunicator", menuName = "UIGameplayStateManagerCommunicator")]
    public class UIGameplayStateManagerCommunicator : ScriptableObject
    {
        private UIGameplayStateManager _manager;
        public void Continue() => _manager.Continue();
        public void SetManager(UIGameplayStateManager manager) => _manager = manager;
    }
}
