using Godot;
using System;

public class MainMenu : Control
{
    private readonly String PLAY_FILE_PATH = "res://Main.tscn";
    private readonly String HOWTOPLAY_FILE_PATH = "res://Scenes/GUI/HowToPlayMenu.tscn";  

    private Button playButton;
    private Button howtToPlayButton;
    private Button quitButton;

    public override void _Ready()
    {
        playButton = (Button)this.FindNode("PlayButton");
        howtToPlayButton = (Button)this.FindNode("HowToPlayButton");
        quitButton = (Button)this.FindNode("QuitButton");

        playButton.Connect("pressed", this, "OnPlayPressed");
        howtToPlayButton.Connect("pressed", this, "OnHowToPlayPressed");
        quitButton.Connect("pressed", this, "OnQuitPressed");
    }

    public void OnPlayPressed()
    {
        this.GetTree().ChangeScene(PLAY_FILE_PATH);
    }

    public void OnHowToPlayPressed()
    {
        this.GetTree().ChangeScene(HOWTOPLAY_FILE_PATH);
    }

    public void OnQuitPressed()
    {
        this.GetTree().Quit();
    }
}
