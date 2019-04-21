using Godot;
using System;

public class RifleAmmo : Ammo
{
    private static readonly int DAMAGE = 10;

    private CollisionShape2D collisionObject;

    private bool launched = false;

    public RifleAmmo(): this(1)
    {
        // Nothing
    }

    public RifleAmmo(int damage): base(damage)
    {
    }

    public override void _Ready()
    {
        collisionObject = (CollisionShape2D)this.GetNode("CollisionObject");

        Connect("body_entered", this, "OnBodyEnter");
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

    private void OnBodyEnter(object body)
    {
        if (!collisionObject.Disabled)
        {
            object obj = GetCollidingBodies()[0];

            if(obj is Character)
            {
                Character character = obj as Character;
                character.Health -= DAMAGE;
            }

            this.QueueFree();
        }
    }
}
