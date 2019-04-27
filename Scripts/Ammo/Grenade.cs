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

        timer = new System.Timers.Timer(TIME);
        timer.Elapsed += OnTimeOver;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    public override void Launch(Vector2 direction, int strength)
    {
        direction = direction.Normalized();
        this.Mode = ModeEnum.Rigid;

        this.ApplyImpulse(Vector2.Zero, direction * strength);
        this.ApplyTorqueImpulse((direction * strength + Vector2.Down * GravityScale * Mass).Angle());

        launched = true;
    }
	
    private void OnTimeOver(System.Object source, ElapsedEventArgs e)
    {
        timer.Stop();
        timer.Dispose();

        foreach (Node node in areaExplosion.GetOverlappingBodies())
        {
            if (node is Character)
            {
                Character character = node as Character;
                character.Health -= DAMAGE;
            }
        }

        ExplosionEffect explosionEffect = GameResources.GetInstance().Get<ExplosionEffect>();
        explosionEffect.SetGlobalPosition(this.GlobalPosition);
        explosionEffect.SetScale(Vector2.One * EXPLOSION_SIZE);
        this.GetTree().GetRoot().GetNode("Main").AddChild(explosionEffect);

        this.QueueFree();
    }
}
