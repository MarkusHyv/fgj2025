using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    private void OnBubbleBurst(BubbleType type, int scoreIncrease)
    {
        Debug.Log($"Player collided with bubble of type {type} and increased score by {scoreIncrease}");
        _player.IncreaseScore(type, scoreIncrease);
    }
}
