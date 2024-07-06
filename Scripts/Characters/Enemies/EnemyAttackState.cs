using DungeonRpg.Scripts.General;
using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{

    private Vector3 targetPosition;


    protected override void EnterState()
    {
        Node3D target = character.AttackArea.GetOverlappingBodies().First();
        targetPosition = target.GlobalPosition;

        character.AnimPlayer.Play(GameConstants.ANIM_ATTACK);
        character.AnimPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        character.AnimPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        character.ToggleHitbox(true);
        Node3D target = character.AttackArea.GetOverlappingBodies().FirstOrDefault();

        // target is not in attack area 
        if (target == null)
        {
            // also not in chase area
            if (character.ChaseArea.GetOverlappingBodies().FirstOrDefault() == null)
            {
                character.StateMachine.SwitchState<EnemyReturnState>();
                return;
            }


            character.StateMachine.SwitchState<EnemyChaseState>();
            return;
        }

        character.AnimPlayer.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;
        Vector3 direction = character.GlobalPosition.DirectionTo(targetPosition);
        character.Sprite3D.FlipH = direction.X < 0;
    }


    private void PerformHit()
    {
        character.ToggleHitbox(false);
        character.Hitbox.GlobalPosition = targetPosition;
    }


}
