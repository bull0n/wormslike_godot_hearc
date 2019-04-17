using Godot;
using System;

public class Grenade : Ammo
{
    private static readonly int RADIUS = 5;
    private static readonly int DAMAGE = 35;

    private bool launched = false;

    public Grenade(): this(1, 1)
    {
        // Nothing
    }

    public Grenade(int radius, int damage): base(radius, damage)
    {
    }

    public override void _Ready()
    {
    }

    public override void _Input(InputEvent inputEvent)
    {
        // Nothing
    }

    public override void Launch(Vector2 direction, int strength)
    {
        direction = direction.Normalized();
        this.Mode = ModeEnum.Rigid;

        this.ApplyImpulse(Vector2.Zero, direction * strength);
        this.ApplyTorqueImpulse((direction * strength + Vector2.Down * GravityScale * Mass).Angle());

        launched = true;
    }
}
