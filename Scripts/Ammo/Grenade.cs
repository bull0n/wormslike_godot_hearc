using Godot;
using System;

public class Grenade : Ammo
{
    private int startTime = 0;

    public Grenade(): this(Vector2.Zero, 1, 1)
    {
        // nothing
    }

    public Grenade(Vector2 direction, int radius, int damage): base(direction, radius, damage)
    {
    }

    public override void _Ready()
    {
        GD.Print("Grenade");
    }

    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent.IsActionReleased("ui_select"))
        {
            GD.Print("Shoot !");
            int timeElapsed = OS.GetSystemTimeSecs() - startTime;
            ApplyImpulse(Vector2.Zero, new Vector2(100, -600 * timeElapsed));
        }

        if (inputEvent.IsActionPressed("ui_select"))
        {
            GD.Print("Start loading...");
            startTime = OS.GetSystemTimeSecs();
        }
    }
}
