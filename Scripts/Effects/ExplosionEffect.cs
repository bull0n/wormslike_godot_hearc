using Godot;
using System;

public class ExplosionEffect : Sprite
{
	private static readonly String ANIM_PLAYER_NODE_NAME = "ExplosionPlayer";
    private static readonly String ANIMATION_NAME = "Explode";
    private static readonly String ANIMATION_FINISHED_SIGNAL = "animation_finished";
	
    public override void _Ready()
    {
        AnimationPlayer animationPlayer = this.GetNode(ANIM_PLAYER_NODE_NAME) as AnimationPlayer;

        animationPlayer.Connect(ANIMATION_FINISHED_SIGNAL, this, nameof(this.OnAnimationFinished));
        animationPlayer.Play(ANIMATION_NAME);
    }

    public void OnAnimationFinished(String animationName)
    {
        this.QueueFree();
    }
}
