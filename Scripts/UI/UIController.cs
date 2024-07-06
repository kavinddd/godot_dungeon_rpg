
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;

    public override void _Ready()
    {
        containers = GetChildren()
            .Where((item) => item is UIContainer)
            .Cast<UIContainer>()
            .ToDictionary((it) => it.Container);

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].Button.Pressed += HandleStartPressed;
    }

    private void HandleStartPressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        GameEvents.RaiseStartGame();
    }
}