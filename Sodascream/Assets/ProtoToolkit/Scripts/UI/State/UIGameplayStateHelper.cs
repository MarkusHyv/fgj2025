using UnityEngine;

namespace ProtoToolkit.Scripts.UI.State
{
    public class UIGamplayStateHelper : MonoBehaviour
    {
        [SerializeField] private UIGameplayStateManagerCommunicator _communicator;

        public void ContinueGameplay()
        {
            _communicator.Continue();
        }
    }
}
