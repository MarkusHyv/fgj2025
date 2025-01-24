using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProtoToolkit.Scripts.UI.State
{
    public class UIGameplayStateManager : MonoBehaviour
    {
        [System.Serializable]
        public struct GameStateToUIState
        {
            public byte GameState;
            public UIStateDefinition Definition;
            public bool WaitForPlayerInput;
        }

        [SerializeField] private bool _debugMode;
        [SerializeField] private UIGameplayStateManagerCommunicator _communicator;
        [SerializeField] private UIStateManager _manager;
        [SerializeField] private List<GameStateToUIState> _stateKeys = new List<GameStateToUIState>();
        private Dictionary<byte, GameStateToUIState> UIStates = new Dictionary<byte, GameStateToUIState>();
        private byte _currentGameState;
        private bool _waitForPlayerInput;
        [SerializeField] private UnityEvent<byte> _onGameStateChangedGameEvent;

        public void OnValidate()
        {
            _manager = GetComponent<UIStateManager>();
        }

        public void Awake()
        {
            if (_debugMode)
            {
                return;
            }
            _communicator?.SetManager(this);
            MapStates();
            _onGameStateChangedGameEvent.AddListener(OnGameStateChanged);
            OnGameStateChanged(0);
        }

        private void OnGameStateChanged(byte obj)
        {
            if (_currentGameState == obj)
                return;
            _currentGameState = obj;
            if (UIStates.TryGetValue(_currentGameState, out var stateDefinition))
            {
                if (!_waitForPlayerInput)
                {
                    _waitForPlayerInput = stateDefinition.WaitForPlayerInput;
                    var transition = new UIStateTransition { Definition = stateDefinition.Definition };
                    _manager.CallStateTransition(transition);
                }
            }
        }

        private void TransitionToState(byte obj)
        {
            if (!UIStates.TryGetValue(_currentGameState, out var stateDefinition))
                return;
            
            var transition = new UIStateTransition { Definition = stateDefinition.Definition };
            _manager.CallStateTransition(transition);
        }

        private void MapStates()
        {
            foreach (var state in _stateKeys)
            {
                UIStates.Add(state.GameState, state);
            }
        }

        public void Continue()
        {
            if (_waitForPlayerInput)
            {
                _waitForPlayerInput = false;
                TransitionToState(_currentGameState);
            }
        }
        
    }
}
