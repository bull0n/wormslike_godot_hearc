using Godot;
using System;

public class Throwable : Ammo
{
    private int startTime = 0;

    public Throwable(): this(1, 1)
    {
        // nothing
    }

    public Throwable(int radius, int damage): base(radius, damage)
    {
    }

    public override void _Ready()
    {
        GD.Print("Throwable");
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
