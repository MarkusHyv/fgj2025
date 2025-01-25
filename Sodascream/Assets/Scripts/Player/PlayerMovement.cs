using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private IInputReader _inputReader;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _speed = 5f;

    private float _turnDirection;

    private void Start()
    {
        _inputReader = GetComponent<IInputReader>();
        _inputReader.OnTurnInput += Turn;
    }

    private void Update()
    {
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
}
