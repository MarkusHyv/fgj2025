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

    public void OnValidate()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        _animator.runtimeAnimatorController = _ducks[Random.Range(0, _ducks.Count - 1)];
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
