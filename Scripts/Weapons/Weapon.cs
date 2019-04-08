using Godot;
using System;

public abstract class Weapon : Node2D
{
    private String weaponName;
    private Ammo weaponAmmo;

    public Weapon(): this("Unknown", null)
    {

    }

    public Weapon(String weaponName, Ammo weaponAmmo)
    {
        this.weaponName = weaponName;
        this.weaponAmmo = weaponAmmo;
    }

    public override void _Ready()
    {
        Ammo ammo = this.GetChild<Ammo>(0);
    }

    public abstract Ammo Load();

    public abstract void StartShoot();
    public abstract void EndShoot();

    public String WeaponName
    {
        get{ return weaponName; }
    }

    public Ammo WeaponAmmo
    {
        get { return weaponAmmo; }
        set { weaponAmmo = value; }
    }
}
