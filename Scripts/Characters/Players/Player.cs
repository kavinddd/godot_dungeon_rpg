using Godot;
using System;
using System.Reflection.Metadata;
using DungeonRpg.Scripts.General;

public partial class Player : CharacterBody3D
{
	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer AnimationPlayer { get; private set; }
	[Export] public Sprite3D Sprite3D { get; private set; }
	[Export] public StateMachine StateMachine { get; private set; }
	public Vector2 Direction;

	public override void _Ready()
	{
	}
	public override void _PhysicsProcess(double delta)
	{

	}

	public override void _Input(InputEvent @event)
	{
		Direction = Input.GetVector(
			GameConstants.INPUT_MOVE_LEFT,
			GameConstants.INPUT_MOVE_RIGHT,
			GameConstants.INPUT_MOVE_FORWARD,
			GameConstants.INPUT_MOVE_BACKWARD);

	}

	public void Flip()
	{
		bool isNotMovingHorizontally = Velocity.X == 0;
		if (isNotMovingHorizontally) return;
		bool isMovingLeft = Velocity.X < 0;
		Sprite3D.FlipH = isMovingLeft;
	}

}
