using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class BubbleConfig
{
    public BubbleType BubbleType;
    public Bubble BubblePrefab;
    public int ScoreValue;
    public float SpeedMultiplier;
    public float SpeedRandomVariance;
    public float SizeMultiplier;
    public float SizeRandomVariance;

}


[CreateAssetMenu(fileName = "BubbleConfig", menuName = "SodaScream/BubbleConfigScriptableObject")]
public class BubbleConfigScriptableObject : ScriptableObject, IBubbleConfigProvider
{
    public float BaseBubbleSpeed;
    public BubbleConfig[] BubbleConfigs;

    public BubbleConfig GetBubbleConfig(BubbleType forType)
    {
        return BubbleConfigs.FirstOrDefault(x => x.BubbleType == forType);
    }
}

public interface IBubbleConfigProvider
{
    BubbleConfig GetBubbleConfig(BubbleType forType);
}
