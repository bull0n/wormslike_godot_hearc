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

    public void Launch()
    {
        this.Mode = ModeEnum.Rigid;

        this.ApplyImpulse(Vector2.Zero, Direction * 5000);

        launched = true;
    }
}