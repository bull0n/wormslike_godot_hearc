using Godot;
using System;

public class RocketLauncher : Weapon
{
    private static readonly String WEAPON_NAME = "Rocket Launcher";
    private static readonly String AMMO_SCENE_PATH = "res://Scenes/Rocket.tscn";

    private static readonly int FORCE_FACTOR = 5;

    private int startTime = 0;

    public RocketLauncher(): base(WEAPON_NAME, null)
    {
    }

    public override void _Ready()
    {

    }

    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent is InputEventMouseMotion eventMouseMotion)
        {
            //Vector2 position = GetGlobalMousePosition();
            //LookAt(position);
            //Scale = new Vector2(1.0f, Mathf.Sign(GetMouseDirection().x));
        }

        if (inputEvent is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.IsActionReleased("shoot"))
            {
                int elapsedTime = OS.GetSystemTimeMsecs() - startTime;

                Vector2 direction = GetMouseDirection();
                Rocket rocket = LoadRocket(direction * elapsedTime * FORCE_FACTOR);
                rocket.Launch();

                startTime = 0;
            }

            if (eventMouseButton.IsActionPressed("shoot"))
            {
                startTime = OS.GetSystemTimeMsecs();
            }
        }
    }

    public Vector2 GetMouseDirection()
    {
        return (GetGlobalMousePosition() - this.GetGlobalPosition()).Normalized();
    }

    public Rocket LoadRocket(Vector2 direction)
    {
        PackedScene rocketScene = (PackedScene)ResourceLoader.Load(AMMO_SCENE_PATH);
        Position2D position = this.GetChild<Position2D>(0);

        Rocket rocket = rocketScene.Instance() as Rocket;
        this.WeaponAmmo = rocket as Ammo;

        rocket.Direction = direction;
        position.AddChild(rocket);

        return rocket;
    }
}
