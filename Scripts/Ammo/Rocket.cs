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

    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent.IsActionPressed("ui_select") && !launched)
        {
            Launch();
        }
    }

    public void Launch()
    {
        Vector2 direction = new Vector2(Vector2.Right);
        direction = direction.Rotated(this.GetParent().GetParent<RocketLauncher>().Rotation);
        GD.Print(this.GetParent().GetParent<RocketLauncher>().Rotation);

        this.Mode = ModeEnum.Rigid;
        this.ApplyImpulse(Vector2.Zero, direction * 5000);

        launched = true;
    }
}