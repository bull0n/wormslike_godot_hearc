/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Represents a rifle
 * *********************************************************************************************************
 */

using Godot;
using System;

public class ExplosionEffect : Sprite
{
	private static readonly String ANIM_PLAYER_NODE_NAME = "ExplosionPlayer";
    private static readonly String ANIMATION_NAME = "Explode";
    private static readonly String ANIMATION_FINISHED_SIGNAL = "animation_finished";
    private Area2D explosionArea;
	private int damage;
	
    /// <summary>
    /// Default constructor
    /// </summary>
	public ExplosionEffect()
	{
		this.damage = 0;
	}
	
    /// <summary>
    /// Initialise the node when the game engine is ready
    /// </summary>
    public override void _Ready()
    {
        AnimationPlayer animationPlayer = this.GetNode(ANIM_PLAYER_NODE_NAME) as AnimationPlayer;

        animationPlayer.Connect(ANIMATION_FINISHED_SIGNAL, this, nameof(this.OnAnimationFinished));
        animationPlayer.Play(ANIMATION_NAME);
        explosionArea = GetNode("ExplosionArea") as Area2D;
        explosionArea.Connect("body_entered", this, "OnBodyEnter");
    }

    /// <summary>
    /// Check collision with terrain and split it if necessary
    /// </summary>
    /// <param name="body"></param>
    public void CollidTerrain(PhysicsBody2D body)
    {
        DescructibleTerrain terrain = body as DescructibleTerrain;

        if(terrain != null)
        {
            this.SplitTerrain(terrain.Split());
        }
    }

    /// <summary>
    /// Split terrains
    /// </summary>
    /// <param name="terrains">Array of terrains to execute the splitting</param>
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

    /// <summary>
    /// Execute physic stuff (collision with terrain)
    /// </summary>
    /// <param name="delta">Time elapsed between the last call of this method</param>
    public override void _PhysicsProcess(float delta)
    {
        var collidingBody = this.explosionArea.GetOverlappingBodies();

        foreach(PhysicsBody2D body in collidingBody)
        {
            this.CollidTerrain(body);
        }
    }

    /// <summary>
    /// Delete the explosion when his animation is terminated
    /// </summary>
    /// <param name="animationName">Name of the animation</param>
    public void OnAnimationFinished(String animationName)
    {
        this.QueueFree();
    }
	
	/// <summary>
    /// Detect collisions when rocket explode and apply damage to characters near the explosion
    /// </summary>
    /// <param name="body">Object which detect collision</param>
    private void OnBodyEnter(object body)
    {
        // Look for characters and remove some health
        foreach (Node node in explosionArea.GetOverlappingBodies())
        {
            if (node is Character)
            {
                Character character = node as Character;
                character.Health -= damage;
            }
        }
    }
	
    /// <summary>
    /// Get and set damage
    /// </summary>
	public int Damage
	{
		get {return this.damage; }
		set {this.damage = value; }
	}
}
