using Godot;
using System;

public partial class Lightning : Ability
{
    public override void _Ready()
    {
        animPlayer.AnimationFinished += (animName) => QueueFree();
    }
}

