using DungeonRpg.Scripts.General;
using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{

    [Export] private Timer idleTimer;
    [Export(PropertyHint.Range, "0,20,0.1")] private float maxIdleTime = 3;
    private int pointIndex = 0;
    protected override void EnterState()
    {
        base.EnterState();
        pointIndex = 1;
        character.AnimPlayer.Play(GameConstants.ANIM_MOVE);
        destination = GetPointGlobalPosition(1);
        character.NavAgent.TargetPosition = destination;
        character.NavAgent.NavigationFinished += HandleNavigationFinished;
        character.ChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
        idleTimer.Timeout += HandleTimeout;
    }

    protected override void ExitState()
    {
        character.NavAgent.NavigationFinished -= HandleNavigationFinished;
        character.ChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
        idleTimer.Timeout -= HandleTimeout;

    }

    public override void _PhysicsProcess(double delta)
    {
        // is idle(ing), then no need to move
        if (!idleTimer.IsStopped()) return;
        Move();
    }


    private void HandleNavigationFinished()
    {
        character.AnimPlayer.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        idleTimer.WaitTime = rng.RandfRange(0, maxIdleTime);
        idleTimer.Start();


    }

    private void HandleTimeout()
    {
        character.AnimPlayer.Play(GameConstants.ANIM_MOVE);

        pointIndex = Mathf.Wrap(
            pointIndex + 1, 0, character.Path.Curve.PointCount
        );

        destination = GetPointGlobalPosition(pointIndex);
        character.NavAgent.TargetPosition = destination;

    }


}
