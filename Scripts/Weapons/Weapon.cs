/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Mother class for all the weapons
 * *********************************************************************************************************
 */

using Godot;
using System;

public abstract class Weapon : Node2D
{
    private String weaponName;
    private Ammo weaponAmmo;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Weapon(): this("Unknown", null)
    {

    }

    /// <summary>
    /// Constructor with weapon name and ammo type
    /// </summary>
    /// <param name="weaponName"></param>
    /// <param name="weaponAmmo"></param>
    public Weapon(String weaponName, Ammo weaponAmmo)
    {
        this.weaponName = weaponName;
        this.weaponAmmo = weaponAmmo;
    }

    /// <summary>
    /// Initialise the node when the game engine is ready
    /// </summary>
    public override void _Ready()
    {
        Ammo ammo = this.GetChild<Ammo>(0);
    }

    /// <summary>
    /// Load the ammo
    /// </summary>
    public abstract void Load();

    /// <summary>
    /// Shoot with the weapon, the time elapsed can be used for changing the strength of the shoot
    /// </summary>
    /// <param name="elapsedTime">Time elapsed between the pression and the release of the button</param>
    public abstract void Shoot(int elapsedTime);

    /// <summary>
    /// Get the mouse direction (normalized) from this node to the mouse position
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMouseDirection()
    {
        return (GetGlobalMousePosition() - this.GetGlobalPosition()).Normalized();
    }
    
    /// <summary>
    /// Get the weapon name
    /// </summary>
    public String WeaponName
    {
        get{ return weaponName; }
    }

    /// <summary>
    /// Get the ammo type
    /// </summary>
    public Ammo WeaponAmmo
    {
        get { return weaponAmmo; }
        set { weaponAmmo = value; }
    }
}
