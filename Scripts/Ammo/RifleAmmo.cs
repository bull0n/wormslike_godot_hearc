using Godot;
using System;

public class RifleAmmo : Ammo
{
    private static readonly int RADIUS = 5;
    private static readonly int DAMAGE = 35;

    private bool launched = false;

    public RifleAmmo(): this(1, 1)
    {
        // Nothing
    }

    public RifleAmmo(int radius, int damage): base(radius, damage)
    {
    }

    public override void _Ready()
    {
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

    public override void Launch(Vector2 direction, int strength)
    {
        direction = direction.Normalized();
        this.Mode = ModeEnum.Rigid;

        this.ApplyImpulse(Vector2.Zero, direction * strength);

        launched = true;
    }
}
