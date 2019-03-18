using Godot;
using System;

public class RocketLauncher : Weapon
{
    private static readonly String WEAPON_NAME = "Rocket Launcher";
    //private static readonly Instance

    public RocketLauncher(): base(WEAPON_NAME, new Rocket())
    {
    }

    public override void _Ready()
    {
        PackedScene rocketScene = (PackedScene)ResourceLoader.Load("res://Scenes/Rocket.tscn");
        Position2D position = this.GetChild<Position2D>(0);
        position.AddChild(rocketScene.Instance());
    }
}
