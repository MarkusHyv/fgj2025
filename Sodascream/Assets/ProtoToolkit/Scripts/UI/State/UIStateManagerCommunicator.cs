using UnityEngine;

namespace ProtoToolkit.Scripts.UI.State
{
    [CreateAssetMenu(fileName = "UIStateManagerCommunicator", menuName = "UIStateManagerCommunicator")]
    public class UIStateManagerCommunicator : ScriptableObject
    {
        private UIStateManager _manager;
        public void TransitionToState(UIStateTransition transition) => _manager?.CallStateTransition(transition);
        public void BackToPreviousState() => _manager?.Back();
        public void SetManager(UIStateManager manager) => _manager = manager;
    }
}
