using System;
using Godot;

public class Rocket : Ammo
{
    private static readonly int RADIUS = 10;
    private static readonly int DAMAGE = 20;

    private bool launched = false;

    public Rocket() : base(RADIUS, DAMAGE)
    {
    }

    public override void _Ready()
    {
        GD.Print("Rocket");
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (launched)
        {
            Vector2 currentDirection = GetLinearVelocity();
            this.LookAt(this.Position + currentDirection);
        }
    }

    public void Launch(Vector2 direction, int strength)
    {
        direction = direction.Normalized();
        this.Mode = ModeEnum.Rigid;

        this.ApplyImpulse(Vector2.Zero, direction * strength);
        this.ApplyTorqueImpulse((direction * strength + Vector2.Down * GravityScale * Mass).Angle());

        launched = true;
    }
}
