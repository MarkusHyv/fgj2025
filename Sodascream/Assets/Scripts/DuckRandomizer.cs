using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuckRandomizer : MonoBehaviour
{
    [SerializeField]
    private List<RuntimeAnimatorController> _ducks;
    [SerializeField]
    private Animator _animator;

    [SerializeField] private bool _resetPositionAfterAnimEnds = true;

    public void OnValidate()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void OnEnable()
    {
        _animator.runtimeAnimatorController = _ducks[Random.Range(0, _ducks.Count - 1)];
    }

    [SerializeField]
    private float previousTime;
    public void LateUpdate()
    {
        if (!_resetPositionAfterAnimEnds) return;
        var state = _animator.GetCurrentAnimatorStateInfo(0);
        var normalizedTime = Mathf.Repeat(state.normalizedTime, 1);
        if (previousTime > normalizedTime)
        {
            _animator.gameObject.transform.localPosition = Vector3.zero;
            _animator.gameObject.transform.localRotation = Quaternion.identity;
        }
        previousTime = normalizedTime;
    }

    public void SetLayerRecursively(Transform t, int layer)
    {
        t.gameObject.layer = layer;
        var childCount = t.childCount;
        for (var i = 0; i < childCount; i++)
        {
            SetLayerRecursively(t.GetChild(i), layer);
        }
    }
}
