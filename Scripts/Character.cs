using Godot;
using System;

public class Character : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private int maxMoveSpeed = 800;
    [Export]
    private int maxFallSpeed = 1500;
    [Export]
    private float friction = 0.4f;

    const int GRAVITY = 98;
    const int ACCEL = 1500;
    const int JUMP_VEL = -1500;
    private Vector2 UP; 
    private Vector2 velocity;
    private Vector2 acceleration;
    private AnimationPlayer animation;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.UP = new Vector2(0, -1);
        this.velocity = new Vector2();
        this.acceleration = new Vector2();

        this.animation = (AnimationPlayer)GetNode("animation_player");
    }

    public override void _PhysicsProcess(float delta)
    {
        acceleration.y = GRAVITY;

        if(Input.IsActionPressed("ui_right"))
        {
            acceleration.x = ACCEL;
        }
        else if(Input.IsActionPressed("ui_left"))
        {
            acceleration.x = -ACCEL;
        }
        else 
        {
            acceleration.x = 0;
        }

        // JUMP WITH GROUND DETECTION

        if(acceleration.x  < 0.2)
        {
            velocity = new Vector2();
        }

        velocity = MoveAndSlide(velocity, UP);
    }

    private void animate()
    {
        if(this.velocity.y > 0 /* GROUND DETECTION */)
        {
            this.animation.Play("fall");
        }
        else if(this.velocity.y < 0/* GROUND DETECTION */)
        {
            this.animation.Play("jump");
        }
        else if(this.velocity.x > 0)
        {
            this.animation.Play("run");
        }
        else
        {
            this.animation.Play("idle");
        }

        // TODO : FLIP_H
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
