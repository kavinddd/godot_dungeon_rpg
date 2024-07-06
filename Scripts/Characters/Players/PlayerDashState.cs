using Godot;
using System;
using DungeonRpg.Scripts.General;

public partial class PlayerDashState : PlayerState
{
	[Export] private Timer dashTimer;
	[Export] private float speed = 20;

	public override void _Ready()
	{
		base._Ready();
		dashTimer.Timeout += HandleDashTimeout;
	}

	public override void _PhysicsProcess(double delta)
	{
		character.MoveAndSlide();
		character.Flip();
	}


	private void HandleDashTimeout()
	{
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
		base.EnterState();
	}
}


