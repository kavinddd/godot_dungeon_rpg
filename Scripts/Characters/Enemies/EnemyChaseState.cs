using DungeonRpg.Scripts.General;
using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{

    // this is for optimization, using GetNextPathPosition every fps for nav is resource-consuming, so we add timer
    [Export] private Timer refreshDestTimer;
    private CharacterBody3D target;


    protected override void EnterState()
    {
        character.AnimPlayer.Play(GameConstants.ANIM_MOVE);
        target = character.ChaseArea
        .GetOverlappingBodies()
        .First() as CharacterBody3D;

        refreshDestTimer.Timeout += HandleRefreshDestinationTimeout;
        character.AttackArea.BodyEntered += HandleAttackAreaEntered;
        character.ChaseArea.BodyExited += HandleChaseAreaExited;
    }



    protected override void ExitState()
    {
        refreshDestTimer.Timeout -= HandleRefreshDestinationTimeout;
        character.AttackArea.BodyEntered -= HandleAttackAreaEntered;
        character.ChaseArea.BodyExited -= HandleChaseAreaExited;
    }


    public override void _PhysicsProcess(double delta)
    {
        character.MoveAndSlide();

    }


    private void HandleRefreshDestinationTimeout()
    {
        destination = target.GlobalPosition;
        character.NavAgent.TargetPosition = destination;
        Move();

    }
    private void HandleAttackAreaEntered(Node3D body)
    {
        GD.Print("attack area entered!");
        character.StateMachine.SwitchState<EnemyAttackState>();
        GD.Print("state is switched to Attack");
    }
    private void HandleChaseAreaExited(Node3D body)
    {
        character.StateMachine.SwitchState<EnemyReturnState>();
    }
}
