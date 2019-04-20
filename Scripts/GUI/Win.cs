using Godot;
using System;

public class Win : Control
{
    private readonly string WIN_MESSSAGE = "has won ! Congratuliations";
    private readonly string MAIN_MENU_PATH = "res://Scenes/GUI/MainMenu.tscn";
    public static int iTeamWon = -1;
    private Button returnMenuButton;
    private Label winLabel;
    private string winMessage;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.winLabel = (Label)this.FindNode("Label");
        this.returnMenuButton = (Button)this.FindNode("ButtonReturnMainMenu");
        this.returnMenuButton.Connect("pressed", this, "OnReturnPressed");

        iTeamWon++;
        winMessage = $"Team {iTeamWon} {WIN_MESSSAGE}";
        this.winLabel.Text = winMessage;
    }

    public void OnReturnPressed()
    {

        this.GetTree().ChangeScene(MAIN_MENU_PATH);
    }


}
