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
    private int timePerRound = 10;

    [Signal]
    public delegate void TimePassedChanged(int timeRemaining);
    [Signal]
    public delegate void ChangeCurrentCharacter(Character character);
    private readonly string WIN_PATH = "res://Scenes/GUI/Win.tscn";

    private bool isRunning;
    private int[] iCurrentPlayer;
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

    static Main()
    {
        // Load data
        GameResources.GetInstance();
    }

    public Main()
    {
        teams = new List<List<Character>>();
        this.timeRemaining = 0;
        this.isRunning = false;
        this.iCurrentPlayer = new int[numberOfTeam];
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
                newPlayer.Connect("CharacterDies", this, "removeCharacter");
                this.teams[i].Add(newPlayer);
                newPlayer.SetPosition(new Vector2(-1300 + i * -1500 + j * 3000, -500));
                this.AddChild(newPlayer);
                newPlayer.SetTeam(currentTeam);
            }
        }

        this.iCurrentTeam = -1;

        for(int team = 0;team < numberOfTeam; team++)
        {
            iCurrentPlayer[team] = -1;
        }

        this.ChooseNextPlayer();
    }

    private void NextTurn()
    {
        this.teams[this.iCurrentTeam][this.iCurrentPlayer[this.iCurrentTeam]].IsActive = false;
        this.ChooseNextPlayer();

        this.TimeRemaining = this.timePerRound;
    }

    private void removeCharacter(Character character)
    {
        if(this.teams[this.iCurrentTeam][this.iCurrentPlayer[this.iCurrentTeam]] == character)
        {
            timeRemaining = 1;
        }
        foreach(List<Character> team in this.teams)
        {
            team.Remove(character);
            
        }

        Grave grave = GameResources.GetInstance().Get<Grave>();
        grave.SetGlobalPosition(character.GlobalPosition);
        this.GetTree().GetRoot().GetNode("Main").AddChild(grave);

        character.QueueFree();

        int numberOfTeamAlive = 0;
        int iTeam = -1;
        for(int i = 0; i < this.teams.Count; i++)
        {
            if(this.teams[i].Count > 0)
            {
                numberOfTeamAlive++;
                iTeam = i;
            }
        }

        if(numberOfTeamAlive == 1)
        {
            this.WinTrigger(iTeam);
        }

        if(this.iCurrentPlayer[iCurrentTeam] != 0)
            this.iCurrentPlayer[iCurrentTeam] -= 1;
    }

    private void WinTrigger(int iTeam)
    {
        Win.iTeamWon = iTeam;
        var nextScene = (PackedScene)ResourceLoader.Load(WIN_PATH);
        GetTree().ChangeSceneTo(nextScene);
    }

    private void ChooseNextPlayer()
    {
        this.iCurrentTeam++;

        if(this.iCurrentTeam >= this.teams.Count)
        {
            this.iCurrentTeam = 0;
        }

        this.iCurrentPlayer[iCurrentTeam]++;

        if (this.iCurrentPlayer[iCurrentTeam] >= this.teams[this.iCurrentTeam].Count)
        {
            this.iCurrentPlayer[iCurrentTeam] = 0;
        }

        this.teams[this.iCurrentTeam][this.iCurrentPlayer[iCurrentTeam]].IsActive = true;
        GD.Print(this.iCurrentTeam + ":" + iCurrentPlayer);
        EmitSignal(nameof(ChangeCurrentCharacter), this.teams[this.iCurrentTeam][this.iCurrentPlayer[iCurrentTeam]]);
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
