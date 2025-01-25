using UnityEngine;

public class GyroCameraController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    [SerializeField] private float _cameraYOffset = 20f;
    [SerializeField] private float _rotationSmoothingSpeed = 5f; // Adjustable smoothing speed
    private bool _adjustForGyro = false;

    private void Start()
    {
        _playerMovement = FindFirstObjectByType<PlayerMovement>();
        _adjustForGyro = _playerMovement.IsUsingGyroInput();
    }
    private void Update()
    {
        if (_playerMovement == null)
        {
            throw new System.Exception($"{nameof(_playerMovement)} not found in {transform.name}");
        }

        this.transform.position = new Vector3(
            _playerMovement.transform.position.x,
            _playerMovement.transform.position.y + _cameraYOffset,
            _playerMovement.transform.position.z
        );

        /*         if (!_adjustForGyro)
                {
                    float playerYRotation = _playerMovement.transform.eulerAngles.y;

                    Quaternion targetRotation = Quaternion.Euler(90f, 0f, -playerYRotation);

                    this.transform.rotation = Quaternion.Slerp(
                        this.transform.rotation,
                        targetRotation,
                        Time.deltaTime * _rotationSmoothingSpeed
                    );
                } */

    }
}
