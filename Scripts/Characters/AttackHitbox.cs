using Godot;
using System;

public partial class AttackHitbox : Area3D, IHitbox
{
    public float GetDamage() => GetOwner<Character>().GetStatResource(Stat.Stength).StatValue;
    public bool CanStun() => false;
}
