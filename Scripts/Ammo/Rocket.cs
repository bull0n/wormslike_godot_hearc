using System;
using Godot;

public class Rocket : Ammo
{
    private static readonly int DAMAGE = 20;

    private CollisionShape2D collisionObject;
    private Area2D areaExplosion;

    private bool launched = false;

    public Rocket() : base(DAMAGE)
    {
    }

    public override void _Ready()
    {
        collisionObject = (CollisionShape2D)this.GetNode("CollisionObject");
        areaExplosion = (Area2D)this.GetNode("AreaExplosion");

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
        this.ApplyTorqueImpulse((direction * strength + Vector2.Down * GravityScale * Mass).Angle());

        launched = true;
    }

    private void OnBodyEnter(object body)
    {
        if (!collisionObject.Disabled)
        {
            foreach (Node node in areaExplosion.GetOverlappingBodies())
            {
                if (node is Character)
                {
                    Character character = node as Character;
                    character.Health -= DAMAGE;
                }
            }

            this.QueueFree();
        }
    }
}
