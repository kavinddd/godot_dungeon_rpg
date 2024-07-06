using System;
using Godot;

public partial class StatLabel : Label
{
    [Export] private StatResource statResource;

    public override void _Ready()
    {
        statResource.OnUpdate += HandleUpdate;
        Text = statResource.StatValue.ToString();
    }

    private void HandleUpdate()
    {
        Text = statResource.StatValue.ToString();
    }
}
