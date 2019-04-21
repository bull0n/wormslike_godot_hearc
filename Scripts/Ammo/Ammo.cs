using Godot;
using System;

public abstract class Ammo : RigidBody2D
{
    private int damage;

    public Ammo(): this(0)
    {
        // Nothing
    }

    public Ammo(int damage)
    {
        this.damage = damage;
    }

    public abstract void Launch(Vector2 direction, int strength);

    public int Damage
    {
        get{return damage;}
    }

    public override void _Ready()
    {
    }
}
