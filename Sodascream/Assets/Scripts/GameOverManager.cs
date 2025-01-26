using System;
using Mono.Cecil.Cil;
using ProtoToolkit.Scripts.DTO;
using ProtoToolkit.Scripts.UI.State;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameOverPopupManager _gameOverPopup;
    [SerializeField] private UIState _gameOverState;
    [SerializeField] private DtoInt _gameOverDto;

    private void Start()
    {
        _gameOverDto.OnValueChanged.AddListener(OnGameOver);
    }

    private void OnGameOver(int arg0)
    {
        _gameOverPopup.Initialize(arg0);
        _gameOverPopup.RequestTransitionFromHudState(_gameOverState.Definition);
    }

}
