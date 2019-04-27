/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Load all scenes data before launching the game
 * *********************************************************************************************************
 */

using System;
using System.Collections.Generic;
using Godot;

class GameResources
{
    private Dictionary<String, PackedScene> data;

    private GameResources()
    {
        data = new Dictionary<String, PackedScene>();

        this.Fill();
    }

    private void Fill()
    {
        data.Add(nameof(HandGrenade),       ResourceLoader.Load<PackedScene>("res://Scenes/Weapons/HandGrenade.tscn"));
        data.Add(nameof(RocketLauncher),    ResourceLoader.Load<PackedScene>("res://Scenes/Weapons/RocketLauncher.tscn"));
        data.Add(nameof(Rifle),             ResourceLoader.Load<PackedScene>("res://Scenes/Weapons/Rifle.tscn"));

        data.Add(nameof(Grenade),           ResourceLoader.Load<PackedScene>("res://Scenes/Ammo/Grenade.tscn"));
        data.Add(nameof(Rocket),            ResourceLoader.Load<PackedScene>("res://Scenes/Ammo/Rocket.tscn"));
        data.Add(nameof(RifleAmmo),         ResourceLoader.Load<PackedScene>("res://Scenes/Ammo/RifleAmmo.tscn"));

        data.Add(nameof(ExplosionEffect),   ResourceLoader.Load<PackedScene>("res://Scenes/Effects/Explosion.tscn"));
    }

    public NodeType Get<NodeType>() where NodeType : class
    {
        String key = typeof(NodeType).Name;
        NodeType node = data[key].Instance() as NodeType;

        if (node == null)
        {
            throw new NullReferenceException("Cannot cast the resource to the given type.");
        }

        return node;
    }

    /*
     * *********************************************************
     * SINGLETON
     * *********************************************************
     */

    private static GameResources instance = null;

    public static GameResources GetInstance()
    {
        if(instance == null)
        {
            instance = new GameResources();
        }

        return instance;
    }
}