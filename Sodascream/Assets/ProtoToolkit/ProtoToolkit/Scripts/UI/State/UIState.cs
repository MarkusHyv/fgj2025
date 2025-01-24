using UnityEngine;
using UnityEngine.Events;

namespace ProtoToolkit.Scripts.UI.State
{
    public class UIState : MonoBehaviour
    {
        [SerializeField] private UIStateManagerCommunicator _communicator;
        [SerializeField] private UIStateDefinition _definition;
        public UIStateDefinition Definition => _definition;
        public UnityEvent<UIState> OnEnter = new UnityEvent<UIState>();
        public UnityEvent<UIState> OnExit = new UnityEvent<UIState>();
        [SerializeField] private bool _enterAnimationImplemented;
        [SerializeField] private bool _exitAnimationImplemented;

        public virtual void EnterState(params object[] args)
        {
            OnEnter.Invoke(this);
            if (!_enterAnimationImplemented)
            {
                gameObject.SetActive(true);
            }
        }

        public void ExitState()
        {
            OnExit.Invoke(this);
            if (!_exitAnimationImplemented)
            {
                gameObject.SetActive(false);
            }
        }

        public void RequestStateTransition(UIStateDefinition definition)
        {
            _communicator.TransitionToState(new UIStateTransition()
            {
                Definition = definition
            });
        }

        public void RequestPreviousState()
        {
            _communicator.BackToPreviousState();
        }

        public void PlayAnimation(Animation anim)
        {
            anim.Play();
        }
    }
}
