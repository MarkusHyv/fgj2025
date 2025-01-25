using System;
using UnityEngine;

public class PlayerCollisionTracker
{
    private BubbleBurster _bubbleBurster;
    private Player _player;

    public PlayerCollisionTracker(BubbleBurster bubbleBurster, Player owner)
    {
        _bubbleBurster = bubbleBurster;
        _bubbleBurster.OnBubbleBurst += OnBubbleBurst;
        _player = owner;
    }

    private void OnBubbleBurst(BubbleType type)
    {
        _player.IncreaseScore(type);
    }
}
