using System;
using System.Diagnostics;
using DungeonRpg.Scripts.General;
using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 destination;
    public override void _Ready()
    {
        base._Ready();
        character.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }


    protected Vector3 GetPointGlobalPosition(int index)
    {
        Vector3 localPos = character.Path.Curve.GetPointPosition(index);
        Vector3 globalPos = character.Path.GlobalPosition;
        return localPos + globalPos;
    }

    protected void Move()
    {
        character.NavAgent.GetNextPathPosition();
        character.Velocity = character.GlobalPosition.DirectionTo(destination);
        character.MoveAndSlide();
        character.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        character.StateMachine.SwitchState<EnemyChaseState>();
    }
    private void HandleZeroHealth()
    {
        character.StateMachine.SwitchState<EnemyDeathState>();
    }
}