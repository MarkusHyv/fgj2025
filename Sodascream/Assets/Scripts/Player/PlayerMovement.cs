using UnityEngine;

public class PlayerMovement : MonoBehaviour, IStoppableElement
{
    private IInputReader _inputReader;
    private IGyroInputReader _gyroInput;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _speed = 5f;
    private bool _useGyroInput;
    [SerializeField] private Vector2 BoundsMin = new Vector2(-70, -36);
    [SerializeField] private Vector2 BoundsMax = new Vector2(70, 36);

    private float _turnDirection;
    private bool _stopped = true;

    private void Start()
    {
        _gyroInput = GetComponent<IGyroInputReader>();
        _useGyroInput = _gyroInput != null && _gyroInput.CanProvideInput();

        if (!_useGyroInput)
        {
            _inputReader = GetComponent<IInputReader>();
            _inputReader.OnTurnInput += Turn;
        }
    }

    private void Update()
    {
        if (_stopped)
        {
            return;
        }
        if (_useGyroInput)
        {
            _turnDirection = _gyroInput.GetGyroTurnDirection();
        }

        Debug.Log("PlayerMovement Update");
        HandleMove();
        HandleRotate();
    }

    private void HandleMove()
    {
        var targetPos = transform.position + transform.forward * _speed * Time.deltaTime;
        targetPos.x = Mathf.Clamp(targetPos.x, BoundsMin.x, BoundsMax.x);
        targetPos.z = Mathf.Clamp(targetPos.z, BoundsMin.y, BoundsMax.y);
        transform.position = targetPos;
    }

    private void HandleRotate()
    {
        var rotationAmount = _turnDirection * _rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }

    private void Turn(float inputValue)
    {
        _turnDirection = inputValue;
    }

    internal bool IsUsingGyroInput() => _useGyroInput;

    public void StopAction()
    {
        _stopped = true;
    }

    public void StartAction()
    {
        Debug.Log("PlayerMovement started");
        _stopped = false;
    }
}
