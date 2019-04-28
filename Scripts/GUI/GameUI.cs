/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Game UI - Timer + weapons list
 * *********************************************************************************************************
 */

using Godot;
using System;

public class GameUI : Control
{
    private Character currentCharacter;
    private Label timerLabel;

    /// <summary>
    /// Initialise the node when game engine is ready
    /// </summary>
    public override void _Ready()
    {
        this.timerLabel = (Label)this.GetNode("Canvas").GetNode("Timer");
    }

    /// <summary>
    /// Update the time label
    /// </summary>
    /// <param name="timeRemaining"></param>
    public void UpdateTime(int timeRemaining)
    {
        this.timerLabel.Text = timeRemaining.ToString();
    }

    /// <summary>
    /// Set the character
    /// </summary>
    /// <param name="character">Character</param>
    public void SetCharacter(Character character)
    {
        this.currentCharacter = character;
    }

    /// <summary>
    /// Change the weapon of the current character on key pressed
    /// </summary>
    /// <param name="inputEvent">Key pressed by the user</param>
    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);

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
