using UnityEngine;
using Unity.Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase _virtualCamera;
    void Start()
    {
        var player = GameObject.FindFirstObjectByType<PlayerMovement>();
        if (player == null)
        {
            throw new System.Exception($"Player reference not found in {transform.name}");
        }

        if (_virtualCamera == null)
        {
            throw new System.Exception($"CinemachineVCam not found in {transform.name}");
        }

        _virtualCamera.Follow = player.transform;
    }

}
