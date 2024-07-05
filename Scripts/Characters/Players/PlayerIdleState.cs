using Godot;
using System;
using DungeonRpg.Scripts.General;

public partial class PlayerIdleState : PlayerState
{

	public override void _PhysicsProcess(double delta)
	{
		if (Player.Direction != Vector2.Zero)
		{
			Player.StateMachine.SwitchState<PlayerMoveState>();
		}
	}

	public override void _Notification(int what)
	{
		base._Notification(what);
		if (what == GameConstants.NOTIFICATION_ENTER_STATE)
		{
			Player.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
			SetPhysicsProcess(true);
			SetProcessInput(true);
		}

		if (what == GameConstants.NOTIFICATION_EXIT_STATE)
		{
			SetPhysicsProcess(false);
			SetProcessInput(false);
		}
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
		Player.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
	}
}
