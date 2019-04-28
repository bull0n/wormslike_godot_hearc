/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Represents a rifle
 * *********************************************************************************************************
 */

using Godot;
using System;

public class Rifle : Weapon
{
    private static String WEAPON_NAME = "Rifle";

    private static readonly int STRENGTH_FACTOR = 5000;
    private Position2D rifleHolder = null;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Rifle(): base(WEAPON_NAME, null)
    {
    }

    /// <summary>
    /// Initialise when the game engine is ready
    /// </summary>
    public override void _Ready()
    {
        rifleHolder = this.GetChild<Position2D>(0);
    }

    /// <summary>
    /// Load the ammo
    /// </summary>
    public override void Load()
    {
        RifleAmmo rifleAmmo = GameResources.GetInstance().Get<RifleAmmo>();
        rifleHolder.AddChild(rifleAmmo);
        this.WeaponAmmo = rifleAmmo;
    }

    /// <summary>
    /// Shoot the ammo
    /// </summary>
    /// <param name="elapsedTime">Time elasped between the pression and the release of the button</param>
    public override void Shoot(int elapsedTime)
    {
        Vector2 direction = GetMouseDirection();

        this.Load();

        rifleHolder.RemoveChild(WeaponAmmo);
        GetTree().GetRoot().AddChild(WeaponAmmo);
        WeaponAmmo.SetGlobalPosition(rifleHolder.GlobalPosition);

        RifleAmmo rifleAmmo = WeaponAmmo as RifleAmmo;
        rifleAmmo.Launch(direction, STRENGTH_FACTOR);
    }
}
