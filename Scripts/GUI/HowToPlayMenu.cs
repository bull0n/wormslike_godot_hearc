using Godot;
using System;

public class HowToPlayMenu : Control
{
    private readonly String MAINMENU_FILE_PATH = "res://Scenes/GUI/MainMenu.tscn";

    private Button okButton;

    public override void _Ready()
    {
        okButton = (Button)this.FindNode("OkButton");

        okButton.Connect("pressed", this, "OnOkPressed");
    }

    public void OnOkPressed()
    {
        this.GetTree().ChangeScene(MAINMENU_FILE_PATH);
    }
}
