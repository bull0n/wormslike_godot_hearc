using Godot;
using System;

public class HandGrenade : Weapon
{
    private static String WEAPON_NAME = "Hand Grenade";

    private static readonly int STRENGTH_FACTOR = 2;
    private Position2D grenadeHolder = null;

    public HandGrenade(): base(WEAPON_NAME, null)
    {
    }

    public override void _Ready()
    {
        grenadeHolder = this.GetChild<Position2D>(0);
    }

    public override void Load()
    {
        Grenade grenade = GameResources.GetInstance().Get<Grenade>();
        grenadeHolder.AddChild(grenade);
        this.WeaponAmmo = grenade;
    }

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
