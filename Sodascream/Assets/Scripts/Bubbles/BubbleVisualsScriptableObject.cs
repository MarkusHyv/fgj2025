using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BubbleVisualsScriptableObject", menuName = "SodaScream/BubbleVisualsScriptableObject")]
public class BubbleVisualsScriptableObject : ScriptableObject, IBubbleVisualsProvider
{
    [SerializeField] private BubbleVisuals[] _bubbleVisuals;

    public BubbleVisuals GetBubbleVisuals(BubbleType bubbleType)
    {
        return _bubbleVisuals.FirstOrDefault(bv => bv.BubbleType == bubbleType);
    }
}

public interface IBubbleVisualsProvider
{
    BubbleVisuals GetBubbleVisuals(BubbleType bubbleType);
}

[Serializable]
public struct BubbleVisuals
{
    public BubbleType BubbleType;
    public Material BaseMaterialForType;
    public Sprite[] FaceSprites;
    public Sprite[] BaseSprites;
    public Sprite[] AccessorySprites;
}
