using Godot;
using System;

public class RocketLauncher : Weapon
{
    private static String WEAPON_NAME = "Rocket Launcher";

    private static readonly int STRENGTH_FACTOR = 5;
    private Position2D rocketHolder = null;

    public RocketLauncher(): base(WEAPON_NAME, null)
    {
    }

    public override void _Ready()
    {
        rocketHolder = this.GetChild<Position2D>(0);
    }

    public override void Load()
    {
        Rocket rocket = GameResources.GetInstance().Get<Rocket>();
        rocketHolder.AddChild(rocket);
        this.WeaponAmmo = rocket;
    }

    public override void Shoot(int elapsedTime)
    {
        Vector2 direction = GetMouseDirection();
        
        this.Load();

        // Remove the rocket from the launcher and set it in the scene as a child of root
        rocketHolder.RemoveChild(WeaponAmmo);
        GetTree().GetRoot().AddChild(WeaponAmmo);
        WeaponAmmo.SetGlobalPosition(rocketHolder.GlobalPosition);

        // Launch the rocket (apply force)
        Rocket rocket = WeaponAmmo as Rocket;
        rocket.Launch(direction, elapsedTime * STRENGTH_FACTOR);
    }
}
