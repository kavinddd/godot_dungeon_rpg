using DungeonRpg.Scripts.General;
using Godot;

public abstract partial class PlayerState : Node
{

	protected Player Player;
	public override void _Ready()
	{
		Player = GetOwner<Player>();
		SetPhysicsProcess(false);
		SetProcessInput(false);
	}

	public override void _Notification(int what)
	{
		base._Notification(what);
		if (what == GameConstants.NOTIFICATION_ENTER_STATE)
		{
			EnterState();
			SetPhysicsProcess(true);
			SetProcessInput(true);
		}

		if (what == GameConstants.NOTIFICATION_EXIT_STATE)
		{
			SetPhysicsProcess(false);
			SetProcessInput(false);
		}
	}

	protected virtual void EnterState() { }


}