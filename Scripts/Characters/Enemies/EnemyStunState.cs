using System;
using DungeonRpg.Scripts.General;
using Godot;

public partial class EnemyStunState : EnemyState
{
    protected override void EnterState()
    {
        character.AnimPlayer.Play(GameConstants.ANIM_STUN);
        character.AnimPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        character.AnimPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (character.AttackArea.HasOverlappingBodies())
        {
            character.StateMachine.SwitchState<EnemyAttackState>();
            return;
        }

        if (character.ChaseArea.HasOverlappingBodies())
        {
            character.StateMachine.SwitchState<EnemyChaseState>();
            return;
        }

        character.StateMachine.SwitchState<EnemyIdleState>();
    }
}