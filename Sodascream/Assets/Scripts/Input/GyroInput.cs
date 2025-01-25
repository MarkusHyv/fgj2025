using UnityEngine;

public class GyroInput : MonoBehaviour, IGyroInputReader
{
    [SerializeField] private bool _useGyroInput;
    [SerializeField] private float _rotationSensitivityModifier = 10f;
    void Start()
    {

        Input.gyro.enabled = _useGyroInput;
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
        return Input.gyro != null && _useGyroInput;
    }

    public float GetGyroTurnDirection()
    {
        float rotationRateZ = Mathf.Clamp(Input.gyro.rotationRateUnbiased.z, -1f, 1f) * _rotationSensitivityModifier * -1f;
        return rotationRateZ;
    }
}