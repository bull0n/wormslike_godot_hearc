using Godot;
using System;

public class Grenade : Ammo
{
    private static readonly int DAMAGE = 35;

    private CollisionShape2D collisionObject;
    private Area2D areaExplosion;

    private bool launched = false;

    public Grenade(): this(1)
    {
    }

    public Grenade(int damage): base(damage)
    {
    }

    public override void _Ready()
    {
        collisionObject = (CollisionShape2D)this.GetNode("CollisionObject");
        areaExplosion = (Area2D)this.GetNode("AreaExplosion");

        Connect("body_entered", this, "OnBodyEnter");
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
        if(!collisionObject.Disabled)
        {
            foreach(Node node in areaExplosion.GetOverlappingBodies())
            {
                if(node is Character)
                {
                    Character character = node as Character;
                    character.Health -= DAMAGE;
                }
            }

            this.QueueFree();
        }
    }
}
