using Godot;
using System;

public class RocketLauncher : Weapon
{
    private static readonly String WEAPON_NAME = "Rocket Launcher";
    private static readonly String AMMO_SCENE_PATH = "res://scenes/Rocket.tscn";

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
            this.Rotation = 0;

            Vector2 direction = eventMouseMotion.GlobalPosition - this.GlobalPosition;
            GD.Print(direction);
            float angle = Vector2.Right.AngleTo(direction.Normalized());
            GD.Print(Mathf.Rad2Deg(angle));
            this.Rotate(angle);
            /*
            Vector2 direction = eventMouseMotion.GlobalPosition - this.GlobalPosition;
            float angle = Vector2.Right.AngleTo(direction.Normalized());

            GD.Print(direction);
            GD.Print(Mathf.Rad2Deg(angle));
            this.Rotate(-angle);
            */
        }

        if (inputEvent is InputEventMouseButton eventMouseButton)
        {
            if(eventMouseButton.IsAction("shoot"))
            {
                Vector2 direction = eventMouseButton.GlobalPosition - this.GlobalPosition;
                Rocket rocket = LoadRocket(direction * 1000);
                rocket.Launch();
            }
        }
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
