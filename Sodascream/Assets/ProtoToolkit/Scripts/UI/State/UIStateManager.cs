using System.Collections.Generic;
using UnityEngine;

namespace ProtoToolkit.Scripts.UI.State
{
    public class UIStateManager : MonoBehaviour
    {
        [SerializeField] private UIStateManagerCommunicator _communicator;
        [SerializeField] private UIStateDefinition _startState;
        [SerializeField] private RectTransform _root;
        [SerializeField] private RectTransform _popups;
        [SerializeField] private UIState[] _precachedStates = new UIState[] {};
        private Stack<UIStateTransition> _history = new Stack<UIStateTransition>();
        private UIStateTransition _currentTransition;
        
        private readonly Dictionary<UIStateDefinition, UIState> _states = new Dictionary<UIStateDefinition, UIState>();
        public UIState CurrentState { get; private set; }

        public void OnValidate()
        {
            _precachedStates = GetComponentsInChildren<UIState>();
            _root = GetComponent<RectTransform>();
        }
        
        public void Awake()
        {
            _communicator.SetManager(this);
            
            foreach (var state in _precachedStates)
            {
                state.gameObject.SetActive(false);
                if (_states.ContainsKey(state.Definition))
                {
                    Debug.LogError($"State already defined within UIStateManager Precached states: {state.Definition}, {state.gameObject.name}");
                    continue;
                }
                _states.Add(state.Definition, state);
            }
            if (_startState == null) return;
            TransitionToState(new UIStateTransition(){ Definition = _startState });
        }

        private void TransitionToState(UIStateTransition transition)
        {
            if (_states.TryGetValue(transition.Definition, out var state) && CurrentState == state)
            {
                return;
            }

            var nextStateIsPopup = state != null && state.Definition.StateType == E_UIStateType.Popup;
            if (CurrentState != null && !nextStateIsPopup)
            {
                CurrentState.ExitState();
            }
            
            if (nextStateIsPopup)
                EnterPopupState(state, transition);
            else
                EnterNormalState(state, transition);
            _currentTransition = transition;
        }

        public void EnterNormalState(UIState state, UIStateTransition transition)
        {
            if (state == null)
            {
                state = Instantiate(transition.Definition.StatePrefab, _root).GetComponent<UIState>();
                state.transform.localPosition = Vector3.zero;
                _states.Add(transition.Definition, state);
            }
            state.gameObject.SetActive(true);
            state.transform.SetAsLastSibling();
            state.EnterState(transition.Parameters);
            CurrentState = state;
        }

        public void EnterPopupState(UIState state, UIStateTransition transition)
        {
            CurrentState = state;
        }
        
        public void CallStateTransition(UIStateTransition transition)
        {
            if (transition.ResetHistory)
            {
                _history.Clear();
            }
            if (transition.WriteToHistory && _currentTransition != null)
                _history.Push(_currentTransition);
            TransitionToState(transition);
        }

        public void Back()
        {
            if (_history.Count < 1)
                return;
            var transition = _history.Count == 1 ? _history.Peek() : _history.Pop();
            TransitionToState(transition);
        }
    }
}
