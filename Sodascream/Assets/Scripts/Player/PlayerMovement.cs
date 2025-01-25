using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    private IInputReader _inputReader;
    private IGyroInputReader _gyroInput;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _speed = 5f;
    private bool _useGyroInput;

    private float _turnDirection;

    private void Start()
    {
        _gyroInput = GetComponent<IGyroInputReader>();
        _useGyroInput = _gyroInput.CanProvideInput();

        if (!_useGyroInput)
        {
            _inputReader = GetComponent<IInputReader>();
            _inputReader.OnTurnInput += Turn;
        }
    }

    private void Update()
    {
        if (_useGyroInput)
        {
            _turnDirection = _gyroInput.GetGyroTurnDirection();
        }
        HandleMove();
        HandleRotate();
    }

    private void HandleMove()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void HandleRotate()
    {
        transform.Rotate(Vector3.up, _turnDirection * _rotationSpeed * Time.deltaTime);
    }

    private void Turn(float inputValue)
    {
        _turnDirection = inputValue;
    }

    public float GetCurrentYRotation()
    {
        return transform.rotation.eulerAngles.y;
    }

}
