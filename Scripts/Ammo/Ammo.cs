using Godot;
using System;

public abstract class Ammo : RigidBody2D
{
    private Vector2 direction;
    private int radius;
    private int damage;

    public Ammo(): this(Vector2.Zero, 0, 0)
    {
        // Nothing
    }

    public Ammo(Vector2 direction, int radius, int damage)
    {
        this.direction = direction;
        this.radius = radius;
        this.damage = damage;
    }

    public Vector2 Direction
    {
        get{return direction;}
        set {   this.direction = value;
                this.direction = this.direction.Normalized();
        }
    }

    public int Radius
    {
        get{return radius;}
    }

    public int Damage
    {
        get{return damage;}
    }

    public override void _Ready()
    {
    }
}
