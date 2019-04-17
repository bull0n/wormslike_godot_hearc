using Godot;
using System;

public class Rifle : Weapon
{
    private static readonly String WEAPON_NAME = "Rifle";
    private static readonly String AMMO_SCENE_PATH = "res://Scenes/Ammo/RifleAmmo.tscn";

    private static readonly int STRENGTH_FACTOR = 5000;
    private Position2D rifleHolder = null;

    public Rifle(): base(WEAPON_NAME, null)
    {
    }

    public override void _Ready()
    {
        rifleHolder = this.GetChild<Position2D>(0);
    }

    public override void Load()
    {
        PackedScene rifleAmmoScene = (PackedScene)ResourceLoader.Load(AMMO_SCENE_PATH);

        RifleAmmo rifleAmmo = rifleAmmoScene.Instance() as RifleAmmo;
        rifleHolder.AddChild(rifleAmmo);
        this.WeaponAmmo = rifleAmmo;
    }

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
