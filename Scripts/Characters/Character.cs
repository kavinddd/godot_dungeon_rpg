using Godot;
using System.Linq;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayer { get; private set; }
    [Export] public Sprite3D Sprite3D { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    [Export] public Area3D Hitbox { get; private set; }
    [Export] public CollisionShape3D HitboxShape { get; private set; }
    [Export] public Area3D Hurtbox { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D Path { get; private set; }
    [Export] public NavigationAgent3D NavAgent { get; private set; }
    [Export] public Area3D ChaseArea { get; private set; }
    [Export] public Area3D AttackArea { get; private set; }


    public Vector2 Direction;

    public override void _Ready()
    {
        Hurtbox.AreaEntered += HandleHurtboxEntered;
        ToggleHitbox(false);
    }


    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;
        if (isNotMovingHorizontally) return;
        bool isMovingLeft = Velocity.X < 0;
        Sprite3D.FlipH = isMovingLeft;
    }
    private void HandleHurtboxEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);
        // the root node of the area that entered hurtbox
        Character hitter = area.GetOwner<Character>();
        health.StatValue -= hitter.GetStatResource(Stat.Stength).StatValue;

        GD.Print($"Hitted Health: {health.StatValue}");

    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where((it) => it.StatType == stat).FirstOrDefault();
    }

    public void ToggleHitbox(bool flag)
    {
        HitboxShape.Disabled = flag;
    }

}