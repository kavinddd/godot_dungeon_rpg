using DungeonRpg.Scripts.General;
using Godot;
using System;

public partial class Bomb : Ability
{
    public override void _Ready()
    {
        animPlayer.AnimationFinished += HandleExpandAnimationFinished;
    }

    private void HandleExpandAnimationFinished(StringName animName)
    {
        if (animName == GameConstants.ANIM_EXPAND) animPlayer.Play(GameConstants.ANIM_EXPLOSION);

        if (animName == GameConstants.ANIM_EXPLOSION) QueueFree();
    }

}
