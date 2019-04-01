using System;
using Godot;

public class Rocket : Ammo
{
    private static readonly int RADIUS = 10;
    private static readonly int DAMAGE = 20;

    private bool launched = false;

    public Rocket() : base(Vector2.Zero, RADIUS, DAMAGE)
    {
    }

    public override void _Ready()
    {
        GD.Print("Rocket");
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        Vector2 direction = GetLinearVelocity();
        this.LookAt(this.Position + direction);
    }

    public void Launch(int strength)
    {
        this.Mode = ModeEnum.Rigid;

        this.ApplyImpulse(Vector2.Zero, Direction * strength);
        this.ApplyTorqueImpulse((Direction * strength + Vector2.Down * GravityScale * Mass).Angle());

        launched = true;
    }
}
