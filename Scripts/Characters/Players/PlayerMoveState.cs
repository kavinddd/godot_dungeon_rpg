using Godot;
using System;
using DungeonRpg.Scripts.General;

public partial class PlayerMoveState : PlayerState
{

	[Export(PropertyHint.Range, "0,20,1")] private float speed = GameConstants.BASE_SPEED;
	public override void _PhysicsProcess(double delta)
	{
		if (character.Direction == Vector2.Zero)
		{
			character.StateMachine.SwitchState<PlayerIdleState>();
			return;
		}

		character.Velocity = new Vector3(character.Direction.X, 0, character.Direction.Y);
		character.Velocity *= speed;

		character.MoveAndSlide();
		character.Flip();

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
		character.AnimPlayer.Play(GameConstants.ANIM_MOVE);
	}
}
