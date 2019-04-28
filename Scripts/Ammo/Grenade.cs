/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Represents a grenade
 * *********************************************************************************************************
 */

using Godot;
using System;
using System.Timers;

public class Grenade : Ammo
{
    private static readonly int DAMAGE = 35;
	private static readonly int EXPLOSION_SIZE = 5;
	private static readonly int TIME = 5000;
	
    private CollisionShape2D collisionObject;
    private Area2D areaExplosion;
    private System.Timers.Timer timer;

    private bool launched = false;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Grenade(): this(1)
    {
    }

    /// <summary>
    /// Constructor with damage
    /// </summary>
    /// <param name="damage">Damage of ammo</param>
    public Grenade(int damage): base(damage)
    {
    }

    /// <summary>
    /// Initialise node when game engine is ready
    /// </summary>
    public override void _Ready()
    {
        collisionObject = (CollisionShape2D)this.GetNode("CollisionObject");
        areaExplosion = (Area2D)this.GetNode("AreaExplosion");

        timer = new System.Timers.Timer(TIME);
        timer.Elapsed += OnTimeOver;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    /// <summary>
    /// Launch the ammo in the given direction with the given strength
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
    /// Explode the grenade when the timer is over
    /// </summary>
    /// <param name="source">Source of the event</param>
    /// <param name="e">Additional information about the event</param>
    private void OnTimeOver(System.Object source, ElapsedEventArgs e)
    {
        timer.Stop();
        timer.Dispose();

        // Look for characters which are hit
        foreach (Node node in areaExplosion.GetOverlappingBodies())
        {
            if (node is Character)
            {
                Character character = node as Character;
                character.Health -= DAMAGE;
            }
        }

        // Execute explosion animation
        ExplosionEffect explosionEffect = GameResources.GetInstance().Get<ExplosionEffect>();
        explosionEffect.SetGlobalPosition(this.GlobalPosition);
        explosionEffect.SetScale(Vector2.One * EXPLOSION_SIZE);
        this.GetTree().GetRoot().GetNode("Main").AddChild(explosionEffect);

        this.QueueFree();
    }
}
