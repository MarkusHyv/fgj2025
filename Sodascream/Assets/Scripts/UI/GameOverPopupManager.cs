using ProtoToolkit.Scripts.DTO;
using ProtoToolkit.Scripts.UI.State;
using TMPro;
using UnityEngine;

public class GameOverPopupManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private UIState _hudState;

    public void RequestTransitionFromHudState(UIStateDefinition toState)
    {
        _hudState.RequestStateTransition(toState);
    }
    public void Initialize(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void OnRestartButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
