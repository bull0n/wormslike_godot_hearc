/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Represents a rocket
 * *********************************************************************************************************
 */

using System;
using Godot;

public class Rocket : Ammo
{
    private static readonly int DAMAGE = 20;
	private static readonly int EXPLOSION_SIZE = 4;

    private CollisionShape2D collisionObject;
    private Area2D areaExplosion;

    private bool launched = false;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Rocket() : base(DAMAGE)
    {
    }

    /// <summary>
    /// Initialise the node when the game engine is ready
    /// </summary>
    public override void _Ready()
    {
        collisionObject = (CollisionShape2D)this.GetNode("CollisionObject");
        areaExplosion = (Area2D)this.GetNode("AreaExplosion");

        Connect("body_entered", this, "OnBodyEnter");
        //areaExplosion.Connect("body_entered", this, "CollidTerrain");
    }

    /// <summary>
    /// Execute physic stuff (change orientation of the rocket)
    /// </summary>
    /// <param name="delta">Time elapsed between this call and the last one</param>
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (launched)
        {
            Vector2 currentDirection = GetLinearVelocity();
            this.LookAt(this.Position + currentDirection);
        }
    }

    /// <summary>
    /// Check collision and split terrain if necessary
    /// </summary>
    /// <param name="body"></param>
    public void CollidTerrain(PhysicsBody2D body)
    {
        DescructibleTerrain terrain = body as DescructibleTerrain;

        if(terrain != null)
        {
            terrain.Split();
        }
    }

    /// <summary>
    /// Launch the rocket in the given direction and with the given strength
    /// </summary>
    /// <param name="direction">Direction of the shoot</param>
    /// <param name="strength">Strength of the shoot</param>
    public override void Launch(Vector2 direction, int strength)
    {
        direction = direction.Normalized();
        this.Mode = ModeEnum.Rigid;

        this.ApplyImpulse(Vector2.Zero, direction * strength);
        this.ApplyTorqueImpulse((direction * strength + Vector2.Down * GravityScale * Mass).Angle());

        launched = true;
    }

    /// <summary>
    /// Detect collisions when rocket explode and apply damage to characters near the explosion
    /// </summary>
    /// <param name="body">Object which detect collision</param>
    private void OnBodyEnter(object body)
    {
        if (!collisionObject.Disabled)
        {
            // Look for characters and remove some health
            foreach (Node node in areaExplosion.GetOverlappingBodies())
            {
                if (node is Character)
                {
                    Character character = node as Character;
                    character.Health -= DAMAGE;
                }
            }

            // Launch the explosion animation
            ExplosionEffect explosionEffect = GameResources.GetInstance().Get<ExplosionEffect>();
            explosionEffect.SetGlobalPosition(this.GlobalPosition);
            explosionEffect.SetScale(Vector2.One * EXPLOSION_SIZE);
            this.GetTree().GetRoot().GetNode("Main").CallDeferred("add_child", explosionEffect);
            //this.GetTree().GetRoot().GetNode("Main").AddChild(explosionEffect);

            this.QueueFree();
        }
    }
}
