using Godot;
using System;

public class GameUI : Control
{
    private Character currentCharacter;
    private Label timerLabel;

    public override void _Ready()
    {
        this.timerLabel = (Label)this.GetNode("Canvas").GetNode("Timer");
    }

    public void UpdateTime(int timeRemaining)
    {
        this.timerLabel.Text = timeRemaining.ToString();
    }

    public void SetCharacter(Character character)
    {
        GD.Print("new character");
        this.currentCharacter = character;
    }

    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);
        //GD.Print("KEY PRESSED : " + inputEvent.ToString());
        //GD.Print("Character : " + currentCharacter);
        if (inputEvent is InputEventKey && this.currentCharacter != null)
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
