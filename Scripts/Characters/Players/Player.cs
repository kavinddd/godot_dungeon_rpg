using Godot;
using System;
using System.Reflection.Metadata;
using DungeonRpg.Scripts.General;

public partial class Player : Character
{

	public override void _Ready()
	{
		base._Ready();
		GameEvents.OnReward += HandleReward;
	}

	public override void _Input(InputEvent @event)
	{
		Direction = Input.GetVector(
			GameConstants.INPUT_MOVE_LEFT,
			GameConstants.INPUT_MOVE_RIGHT,
			GameConstants.INPUT_MOVE_FORWARD,
			GameConstants.INPUT_MOVE_BACKWARD);

	}
	private void HandleReward(RewardResource reward)
	{
		StatResource targetStat = GetStatResource(reward.TargetStat);

		targetStat.StatValue += reward.Amount;
	}



}
