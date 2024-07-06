using DungeonRpg.Scripts.General;
using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        character.AnimPlayer.Play(GameConstants.ANIM_IDLE);
        character.ChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        character.ChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        character.StateMachine.SwitchState<EnemyReturnState>();
    }
}
