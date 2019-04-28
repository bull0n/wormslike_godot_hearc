/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Mother class of all the ammo
 * *********************************************************************************************************
 */

using Godot;
using System;

public abstract class Ammo : RigidBody2D
{
    private int damage;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Ammo(): this(0)
    {
        // Nothing
    }

    /// <summary>
    /// Constructor with damage
    /// </summary>
    /// <param name="damage">Damage of ammo</param>
    public Ammo(int damage)
    {
        this.damage = damage;
    }

    /// <summary>
    /// Apply force to this ammo
    /// </summary>
    /// <param name="direction">Direction to shoot the ammo</param>
    /// <param name="strength">Strength of the shoot</param>
    public abstract void Launch(Vector2 direction, int strength);

    /// <summary>
    /// Get damage of this ammo
    /// </summary>
    public int Damage
    {
        get{return damage;}
    }
}
