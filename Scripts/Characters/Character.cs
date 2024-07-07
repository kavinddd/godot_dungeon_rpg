using Godot;
using System;
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
    [Export] public Timer IFrameTimer { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D Path { get; private set; }
    [Export] public NavigationAgent3D NavAgent { get; private set; }
    [Export] public Area3D ChaseArea { get; private set; }
    [Export] public Area3D AttackArea { get; private set; }

    public Vector2 Direction;
    private ShaderMaterial shader;

    public override void _Ready()
    {
        shader = (ShaderMaterial)Sprite3D.MaterialOverlay;

        Hurtbox.AreaEntered += HandleHurtboxEntered;

        Sprite3D.TextureChanged += HandleTextureChanged;

        IFrameTimer.Timeout += HandleTimeout;

        ToggleHitbox(false);
    }

    private void HandleTimeout()
    {
        shader.SetShaderParameter("active", false);
        // commented this out, if this is disabled, other methods that are trying to use BodyEntered signal is throwing an error
        // Hurtbox.GetChild<CollisionShape3D>(0).Disabled = false; 
    }

    private void HandleTextureChanged()
    {
        shader.SetShaderParameter("tex", Sprite3D.Texture);
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
        // if area is not AttackHitbox, then return, otherwise, create a variable hitbox
        if (area is not IHitbox hitbox) return;

        StatResource health = GetStatResource(Stat.Health);
        health.StatValue -= hitbox.GetDamage();

        GD.Print($"Hitted Health: {health.StatValue}");
        // so the character turn red
        shader.SetShaderParameter("active", true);
        // commented this out, if this is disabled, other methods that are trying to use BodyEntered signal is throwing an error
        // Hurtbox.GetChild<CollisionShape3D>(0).Disabled = true; 
        IFrameTimer.Start();

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