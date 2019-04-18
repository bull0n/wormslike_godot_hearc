using Godot;
using System;

public class GameUI : Control
{
    private int currentWeaponIndex = 0;
    private Character currentCharacter;

    public override void _Ready()
    {
        GD.Print(GetTree().GetRoot().GetChild(0).GetChild(1).Name);
		currentCharacter = (Character)GetTree().GetRoot().GetChild(0).GetChild(1);
    }

    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);

        if(inputEvent is InputEventKey)
        {
            InputEventKey keyEvent = inputEvent as InputEventKey;

            if(keyEvent.IsActionPressed("rifle"))
            {
                currentCharacter.SelectedWeapon = Character.SelectableWeapon.Rifle;
            }
            else if(keyEvent.IsActionPressed("rocket_launcher"))
            {
                currentCharacter.SelectedWeapon = Character.SelectableWeapon.Bazooka;
            }
            else if (keyEvent.IsActionPressed("grenade"))
            {
                currentCharacter.SelectedWeapon = Character.SelectableWeapon.Grenade;
            }
        }
    }
}
