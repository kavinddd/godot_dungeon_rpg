using DungeonRpg.Scripts.General;
using Godot;
using System;
public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimer;
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
        comboTimer.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        character.AnimPlayer.Play(GameConstants.ANIM_ATTACK + comboCounter, -1, 1.5f);
        character.AnimPlayer.AnimationFinished += HandleAnimationFinished;

    }

    protected override void ExitState()
    {
        character.AnimPlayer.AnimationFinished -= HandleAnimationFinished;
        comboTimer.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);
        character.StateMachine.SwitchState<PlayerIdleState>();
        character.ToggleHitbox(true);
    }
    private void PerformHit()
    {
        Vector3 newPos = character.Sprite3D.FlipH ? Vector3.Left : Vector3.Right;
        float distanceMultiplier = 0.75f;
        character.Hitbox.Position = newPos * distanceMultiplier;
        character.ToggleHitbox(false);
        // character.Hitbox.Transform.TranslatedLocal(newPos);
    }


}