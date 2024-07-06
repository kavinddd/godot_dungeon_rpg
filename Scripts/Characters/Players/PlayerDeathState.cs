using System;
using DungeonRpg.Scripts.General;
using Godot;
public partial class PlayerDeathState : PlayerState
{
    protected override void EnterState()
    {
        GD.Print("Enter death state");
        character.AnimPlayer.Play(GameConstants.ANIM_DEATH);
        character.AnimPlayer.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        character.QueueFree();
    }

}