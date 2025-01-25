using System.Collections.Generic;
using UnityEngine;

public class DuckRandomizer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _ducks;
    [SerializeField]
    private Transform _root;

    public void OnEnable()
    {
        var childCount = _root.childCount;
        for (var i = 0; i < childCount - 1; i++)
        {
            Destroy(_root.GetChild(i).gameObject);
        }

        var index = Random.Range(0, _ducks.Count - 1);
        var duck = Instantiate(_ducks[index], _root);
        duck.transform.localPosition = Vector3.zero;
        duck.transform.localRotation = Quaternion.identity;
        duck.transform.localScale = Vector3.one;
        SetLayerRecursively(duck.transform, LayerMask.NameToLayer("Twerk"));
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
