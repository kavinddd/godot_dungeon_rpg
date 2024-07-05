using Godot;
using System;
using DungeonRpg.Scripts.General;

public partial class PlayerMoveState : PlayerState
{

	[Export(PropertyHint.Range, "0,20,1")] private float speed = GameConstants.BASE_SPEED;
	public override void _PhysicsProcess(double delta)
	{
		GD.Print("Move");
		if (Player.Direction == Vector2.Zero)
		{
			Player.StateMachine.SwitchState<PlayerIdleState>();
			return;
		}

		Player.Velocity = new Vector3(Player.Direction.X, 0, Player.Direction.Y);
		Player.Velocity *= speed;

		Player.MoveAndSlide();
		Player.Flip();

	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
		{
			Player.StateMachine.SwitchState<PlayerDashState>();
		}
	}
	protected override void EnterState()
	{
		base.EnterState();
		Player.AnimationPlayer.Play(GameConstants.ANIM_WALKING);
	}
}
