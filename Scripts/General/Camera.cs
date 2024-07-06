using System;
using Godot;

public partial class Camera : Camera3D
{
    [Export] private Node target;
    [Export] private Vector3 positionAwayFromTarget;
    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnStartGame += () => GD.Print("Second subscription");
        GameEvents.OnEndGame += HandleEndGame;
    }



    private void HandleStartGame()
    {
        Reparent(target);
        Position = positionAwayFromTarget;
    }
    private void HandleEndGame()
    {
        Node rootNodeScene = GetTree().CurrentScene;
        Reparent(rootNodeScene);
    }
}