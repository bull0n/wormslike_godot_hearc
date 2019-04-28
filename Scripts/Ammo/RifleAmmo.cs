/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Represents a rifle ammo
 * *********************************************************************************************************
 */

using Godot;
using System;

public class RifleAmmo : Ammo
{
    private static readonly int DAMAGE = 10;

    private CollisionShape2D collisionObject;

    private bool launched = false;

    /// <summary>
    /// Default constructor
    /// </summary>
    public RifleAmmo(): this(1)
    {
        // Nothing
    }

    /// <summary>
    /// Constructor with damage
    /// </summary>
    /// <param name="damage">Damage of ammo</param>
    public RifleAmmo(int damage): base(damage)
    {
    }

    /// <summary>
    /// Initialise the node when the game engine is ready
    /// </summary>
    public override void _Ready()
    {
        collisionObject = (CollisionShape2D)this.GetNode("CollisionObject");

        Connect("body_entered", this, "OnBodyEnter");
    }
	
    /// <summary>
    /// Execute physic stuff (change orientation of the bullet)
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
    /// Launch the ammo
    /// </summary>
    /// <param name="direction">Direction of the shoot</param>
    /// <param name="strength">Strength of the shoot</param>
    public override void Launch(Vector2 direction, int strength)
    {
        direction = direction.Normalized();
        this.Mode = ModeEnum.Rigid;

        this.ApplyImpulse(Vector2.Zero, direction * strength);

        launched = true;
    }

    /// <summary>
    /// Detect collisions with explosion area
    /// </summary>
    /// <param name="body">Object who sent the event</param>
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
