using Godot;
using System;

public class RocketLauncher : Weapon
{
    private static readonly String WEAPON_NAME = "Rocket Launcher";
    private static readonly String AMMO_SCENE_PATH = "res://Scenes/Rocket.tscn";

    private static readonly int STRENGTH_FACTOR = 5;

    private int startTime = 0;
    private Position2D rocketHolder = null;
    private Rocket rocket = null;

    public RocketLauncher(): base(WEAPON_NAME, null)
    {
    }

    public override void _Ready()
    {
        rocketHolder = this.GetChild<Position2D>(0);
    }

    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent is InputEventMouseMotion eventMouseMotion)
        {
            Vector2 position = GetGlobalMousePosition();
            LookAt(position);
            Scale = new Vector2(1.0f, Mathf.Sign(GetMouseDirection().x));
        }

        if (inputEvent is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.IsActionReleased("shoot"))
            {
                EndShoot();
            }

            if (eventMouseButton.IsActionPressed("shoot"))
            {
                StartShoot();
            }
        }
    }

    public override Ammo Load()
    {
        PackedScene rocketScene = (PackedScene)ResourceLoader.Load(AMMO_SCENE_PATH);

        Rocket rocket = rocketScene.Instance() as Rocket;
        rocketHolder.AddChild(rocket);
        this.WeaponAmmo = rocket;

        return rocket;
    }

    public override void StartShoot()
    {
        rocket = Load() as Rocket;
        GD.Print(rocket);
        startTime = OS.GetSystemTimeMsecs();
    }

    public override void EndShoot()
    {
        int elapsedTime = OS.GetSystemTimeMsecs() - startTime;
        Vector2 direction = GetMouseDirection();
        
        // Remove the rocket from the launcher and set it in the scene as a child of root
        rocketHolder.RemoveChild(rocket);
        GetTree().GetRoot().AddChild(rocket);
        rocket.SetGlobalPosition(rocketHolder.GlobalPosition);

        // Launch the rocket (apply force)
        rocket.Launch(direction, elapsedTime * STRENGTH_FACTOR);

        startTime = 0;
    }

    public Vector2 GetMouseDirection()
    {
        return (GetGlobalMousePosition() - this.GetGlobalPosition()).Normalized();
    }
}
