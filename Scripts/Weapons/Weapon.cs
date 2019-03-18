using Godot;
using System;

public class Weapon : Node2D
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
