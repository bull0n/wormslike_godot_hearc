/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Represents a rocket launcher
 * *********************************************************************************************************
 */

using Godot;
using System;

public class RocketLauncher : Weapon
{
    private static String WEAPON_NAME = "Rocket Launcher";

    private static readonly int STRENGTH_FACTOR = 5;
    private Position2D rocketHolder = null;

    /// <summary>
    /// Default constructor
    /// </summary>
    public RocketLauncher(): base(WEAPON_NAME, null)
    {
    }

    /// <summary>
    /// Initialise node when game engine is ready
    /// </summary>
    public override void _Ready()
    {
        rocketHolder = this.GetChild<Position2D>(0);
    }

    /// <summary>
    /// Load the rocket inside the rocket launcher
    /// </summary>
    public override void Load()
    {
        Rocket rocket = GameResources.GetInstance().Get<Rocket>();
        rocketHolder.AddChild(rocket);
        this.WeaponAmmo = rocket;
    }

    /// <summary>
    /// Shoot the rocket
    /// </summary>
    /// <param name="elapsedTime">Elapsed time between the pression and the release of the button</param>
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
