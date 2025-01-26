using ProtoToolkit.Scripts.DTO;
using UnityEngine;

public class StartOnGameStart : MonoBehaviour
{
    public void StartAction(int arg0)
    {
        Debug.Log("StartAction called in StartOnGameStart");
        foreach (var stoppableElement in GetComponentsInChildren<IStoppableElement>())
        {
            stoppableElement.StartAction();
        }
    }
}
