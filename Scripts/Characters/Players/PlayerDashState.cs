using Godot;
using System;
using DungeonRpg.Scripts.General;

public partial class PlayerDashState : PlayerState
{
	[Export] private Timer dashTimer;
	[Export] private Timer cooldownTimer;
	[Export] private float speed = 20;
	[Export] private PackedScene bombScene;

	public override void _Ready()
	{
		base._Ready();
		dashTimer.Timeout += HandleDashTimeout;
		CanTransition = () => cooldownTimer.IsStopped();
	}

	public override void _PhysicsProcess(double delta)
	{
		character.MoveAndSlide();
		character.Flip();
	}


	private void HandleDashTimeout()
	{
		cooldownTimer.Start();
		character.Velocity = Vector3.Zero;
		character.StateMachine.SwitchState<PlayerIdleState>();
	}

	protected override void EnterState()
	{
		character.AnimPlayer.Play(GameConstants.ANIM_DASH);
		character.Velocity = new(
			character.Direction.X, 0, character.Direction.Y);

		if (character.Velocity == Vector3.Zero)
		{
			character.Velocity = character.Sprite3D.FlipH ? Vector3.Left : Vector3.Right;
		}

		character.Velocity *= speed;
		dashTimer.Start();
		Node3D bomb = bombScene.Instantiate<Node3D>();
		GetTree().CurrentScene.AddChild(bomb);
		bomb.GlobalPosition = character.GlobalPosition;
	}
}


