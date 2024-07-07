using DungeonRpg.Scripts.General;
using Godot;

public partial class TreasureChest : StaticBody3D
{
    [Export] private Area3D area;
    [Export] private Sprite3D sprite;
    [Export] private RewardResource reward;

    public override void _Ready()
    {
        area.BodyEntered += (_) => sprite.Visible = true;
        area.BodyExited += (_) => sprite.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (!area.Monitoring
        || !area.HasOverlappingBodies()
        || !Input.IsActionJustPressed(GameConstants.INPUT_INTERACT)) return;

        area.Monitoring = false;
        GameEvents.RaiseReward(reward);
    }
}