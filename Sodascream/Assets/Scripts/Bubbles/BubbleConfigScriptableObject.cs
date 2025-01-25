using System;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEngine;

[Serializable]
public class BubbleConfig
{
    public BubbleType BubbleType;
    public Bubble BubblePrefab;
    public int ScoreValue;
    [Tooltip("The amount of health the player gains when they burst this bubble")]
    public int BubbleLifeIncrease;
    [Tooltip("The amount of health the player gains when they hit this bubble with their body")]
    public int BubbleLifeDecrease;
    public float SpeedMultiplier;
    public float SpeedRandomVariance;
    public float SizeMultiplier;
    public float SizeRandomVariance;

}


[CreateAssetMenu(fileName = "BubbleConfig", menuName = "SodaScream/BubbleConfigScriptableObject")]
public class BubbleConfigScriptableObject : ScriptableObject, IBubbleConfigProvider
{
    public float BaseBubbleSpeed;
    public float BubbleLifeTimeInSeconds;
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
