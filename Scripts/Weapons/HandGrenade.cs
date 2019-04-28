/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Represents a hand grenade (only use for keeping the same structure as rocket launcher and rifle)
 * *********************************************************************************************************
 */

using Godot;
using System;

public class HandGrenade : Weapon
{
    private static String WEAPON_NAME = "Hand Grenade";

    private static readonly int STRENGTH_FACTOR = 2;
    private Position2D grenadeHolder = null;

    /// <summary>
    /// Default constructor
    /// </summary>
    public HandGrenade(): base(WEAPON_NAME, null)
    {
    }

    /// <summary>
    /// Initialise when the game engine is ready
    /// </summary>
    public override void _Ready()
    {
        grenadeHolder = this.GetChild<Position2D>(0);
    }

    /// <summary>
    /// Load the grenade
    /// </summary>
    public override void Load()
    {
        Grenade grenade = GameResources.GetInstance().Get<Grenade>();
        grenadeHolder.AddChild(grenade);
        this.WeaponAmmo = grenade;
    }

    /// <summary>
    /// Throw the grenade
    /// </summary>
    /// <param name="elapsedTime">Elapsed time between the pression and the release of the button</param>
    public override void Shoot(int elapsedTime)
    {
        Vector2 direction = GetMouseDirection();

        this.Load();

        grenadeHolder.RemoveChild(WeaponAmmo);
        GetTree().GetRoot().AddChild(WeaponAmmo);
        WeaponAmmo.SetGlobalPosition(grenadeHolder.GlobalPosition);

        Grenade grenade = WeaponAmmo as Grenade;
        grenade.Launch(direction, elapsedTime * STRENGTH_FACTOR);
    }
}
