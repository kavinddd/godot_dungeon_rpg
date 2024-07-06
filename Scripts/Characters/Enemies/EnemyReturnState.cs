using DungeonRpg.Scripts.General;
using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();
        destination = GetPointGlobalPosition(0);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (character.NavAgent.IsNavigationFinished())
        {
            character.StateMachine.SwitchState<EnemyPatrolState>();
            return;
        };

        Move();

    }
    protected override void EnterState()
    {
        character.AnimPlayer.Play(GameConstants.ANIM_MOVE);
        character.NavAgent.TargetPosition = destination;
        character.ChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
    }
    protected override void ExitState()
    {
        character.ChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
    }

}
