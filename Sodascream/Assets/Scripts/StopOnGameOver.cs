using ProtoToolkit.Scripts.DTO;
using UnityEngine;

public class StopOnGameOver : MonoBehaviour
{
    [SerializeField] private DtoInt _gameOverDto;

    private void Start()
    {
        _gameOverDto.OnValueChanged.AddListener(Stop);
    }

    private void Stop(int arg0)
    {
        foreach (var stoppableElement in GetComponentsInChildren<IStoppableElement>())
        {
            stoppableElement.StopAction();
        }
    }
}