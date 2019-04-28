using Godot;
using System;

public class ExplosionEffect : Sprite
{
	private static readonly String ANIM_PLAYER_NODE_NAME = "ExplosionPlayer";
    private static readonly String ANIMATION_NAME = "Explode";
    private static readonly String ANIMATION_FINISHED_SIGNAL = "animation_finished";
    private Area2D explosionArea;
	
    public override void _Ready()
    {
        AnimationPlayer animationPlayer = this.GetNode(ANIM_PLAYER_NODE_NAME) as AnimationPlayer;

        animationPlayer.Connect(ANIMATION_FINISHED_SIGNAL, this, nameof(this.OnAnimationFinished));
        animationPlayer.Play(ANIMATION_NAME);
        explosionArea = GetNode("ExplosionArea") as Area2D;
        //explosionArea.Connect("body_entered", this, "CollidTerrain");
    }

    public void CollidTerrain(PhysicsBody2D body)
    {
        DescructibleTerrain terrain = body as DescructibleTerrain;

        if(terrain != null)
        {
            this.SplitTerrain(terrain.Split());
        }
    }

    public void SplitTerrain(DescructibleTerrain[] terrains)
    {
        for(int i = 0; terrains != null && i < terrains.Length; i++)
        {
            //if(this.explosionArea.OverlapsBody(terrains[i]))
            //{
                var newTerrains = terrains[i].Split();
                this.SplitTerrain(terrains[i].Split());
            //}
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        var collidingBody = this.explosionArea.GetOverlappingBodies();

        foreach(PhysicsBody2D body in collidingBody)
        {
            this.CollidTerrain(body);
        }
    }

    public void OnAnimationFinished(String animationName)
    {
        this.QueueFree();
    }
}
