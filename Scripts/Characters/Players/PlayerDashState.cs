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
		Player.MoveAndSlide();
		Player.Flip();
	}


	private void HandleDashTimeout()
	{
		Player.Velocity = Vector3.Zero;
		Player.StateMachine.SwitchState<PlayerIdleState>();
	}

	protected override void EnterState()
	{
		Player.AnimationPlayer.Play(GameConstants.ANIM_DASH);
		Player.Velocity = new(
			Player.Direction.X, 0, Player.Direction.Y);

		if (Player.Velocity == Vector3.Zero)
		{
			Player.Velocity = Player.Sprite3D.FlipH ? Vector3.Left : Vector3.Right;
		}

		Player.Velocity *= speed;
		dashTimer.Start();
		base.EnterState();
	}
}


