using Godot;
using System;

public abstract class Ammo : RigidBody2D
{
    private int radius;
    private int damage;

    public Ammo(int radius, int damage)
    {
        this.radius = radius;
        this.damage = damage;
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
        GD.Print("Yolo");
    }
}
