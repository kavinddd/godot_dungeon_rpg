using System;
using DungeonRpg.Scripts.General;
using Godot;
public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        character.AnimPlayer.Play(GameConstants.ANIM_DEATH);
        character.AnimPlayer.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        character.QueueFree();
    }

}