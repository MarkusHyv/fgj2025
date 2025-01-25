// Create a cube with camera vector names on the faces.
// Allow the device to show named faces as it is oriented.

using System;
using UnityEngine;

public class GyroInput : MonoBehaviour, IGyroInputReader
{
    [SerializeField] private float _rotationSensitivityModifier = 10f;
    private Quaternion _resetValue;
    void Start()
    {
        Input.gyro.enabled = true;
    }

    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
        GUILayout.Label("RotationRate unbiased: " + Input.gyro.rotationRateUnbiased);
    }

    public bool CanProvideInput()
    {
        return Input.gyro != null;
    }

    public float GetGyroTurnDirection()
    {
        float rotationRateZ = Mathf.Clamp(Input.gyro.rotationRateUnbiased.z, -1f, 1f) * _rotationSensitivityModifier * -1f;
        return rotationRateZ;
    }
}