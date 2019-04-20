using Godot;
using System;
using System.Collections.Generic;

public class Main : Node
{
    [Export]
    public int numberOfPlayerPerTeam = 2;
    [Export]
    public int numberOfTeam = 2;
    [Export]
    private int timePerRound = 5;

    [Signal]
    public delegate void TimePassedChanged(int timeRemaining);
    [Signal]
    public delegate void ChangeCurrentCharacter(Character character);

    private bool isRunning;
    private int iCurrentPlayer;
    private int iCurrentTeam;
    

    private List<List<Character>> teams;
    private float timeRemaining;
    public float TimeRemaining 
    { 
        get {return this.timeRemaining;} 
        set
        {
            this.timeRemaining = value;
            EmitSignal(nameof(TimePassedChanged), (int)this.timeRemaining);
        } 
    }

    public Main()
    {
        teams = new List<List<Character>>();
        this.timeRemaining = 0;
        this.isRunning = false;
    }

    public void Init()
    {
        this.SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var characterScene = GD.Load<PackedScene>("res://Scenes/Character.tscn"); 

        for(int i = 0; i < this.numberOfTeam; i++)
        {
            Character.Team currentTeam = (Character.Team)i;
            this.teams.Add(new List<Character>());

            for(int j = 0; j < this.numberOfPlayerPerTeam; j++)
            {
                Character newPlayer = (Character)characterScene.Instance();
                this.teams[i].Add(newPlayer);
                newPlayer.SetPosition(new Vector2(-290, -90));
                this.AddChild(newPlayer);
                newPlayer.SetTeam(currentTeam);
            }
        }

        this.iCurrentTeam = 0;
        this.iCurrentPlayer = 0;

        EmitSignal(nameof(ChangeCurrentCharacter), this.teams[0][0]);
        GD.Print("hello");
    }

    private void NextTurn()
    {
        this.ChooseNextPlayer();


        this.TimeRemaining = this.timePerRound;
    }

    private void ChooseNextPlayer()
    {
        this.iCurrentPlayer++;
        this.iCurrentTeam++;

        if(this.iCurrentTeam == this.teams.Count)
        {
            this.iCurrentTeam = 0;
        }
        if(this.iCurrentPlayer == this.teams[this.iCurrentTeam].Count)
        {
            this.iCurrentPlayer = 0;
        }

        EmitSignal(nameof(ChangeCurrentCharacter), this.teams[this.iCurrentTeam][this.iCurrentPlayer]);
    }

    private void Start()
    {
        this.timeRemaining = this.timePerRound;
        this.isRunning = true;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.ConnectSignal();
        this.SpawnPlayer();
        this.Start();
    }

    private void ConnectSignal()
    {
        Node gameUI = this.GetNode("MainCamera").GetNode("GameUI"); 
        Connect(nameof(TimePassedChanged), gameUI, "UpdateTime");
        Connect(nameof(ChangeCurrentCharacter), gameUI, "SetCharacter");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(this.isRunning)
        {
            this.TimeRemaining -= delta;
            
            if(this.timeRemaining <= 0)
            {
                this.NextTurn();
            }            
        }
    }
}
