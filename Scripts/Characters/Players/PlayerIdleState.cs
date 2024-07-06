using Godot;
using System;
using DungeonRpg.Scripts.General;

public partial class PlayerIdleState : PlayerState
{

	public override void _PhysicsProcess(double delta)
	{
		if (character.Direction != Vector2.Zero)
		{
			character.StateMachine.SwitchState<PlayerMoveState>();
		}
	}

	public override void _Input(InputEvent @event)
	{
		CheckForAttackInput();
		if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
		{
			character.StateMachine.SwitchState<PlayerDashState>();
		}
	}

	protected override void EnterState()
	{
		character.AnimPlayer.Play(GameConstants.ANIM_IDLE);
	}
}
