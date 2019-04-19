using Godot;
using System;

public class GameUI : Control
{
    private int currentWeaponIndex = 0;
    private Character currentCharacter;
    private Label timerLabel;

    public override void _Ready()
    {
        this.timerLabel = (Label)this.GetNode("Canvas").GetNode("Timer");
    }

    public void updateTime(int timeRemaining)
    {
        this.timerLabel.Text = timeRemaining.ToString();
    }

    public void setCharacter(Character character)
    {
        GD.Print("new character");
        this.currentCharacter = character;
    }

    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);

        if(inputEvent is InputEventKey && this.currentCharacter != null)
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
