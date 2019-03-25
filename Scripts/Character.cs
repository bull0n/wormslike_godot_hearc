using Godot;
using System;

public class Character : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private int MOVE_SPEED = 300;
    [Export]
    private int MAX_FALL_SPEED = 9000;
    [Export]
    private int MAX_RUN_SPEED = 3000;


    const int GRAVITY = 980;
    const int JUMP_VEL = -150000;
    const int ACCEL = 1000;
    const float FRICTION = 0.4f;
    private Vector2 UP; 
    private Vector2 velocity;
    private Vector2 acceleration;
    private AnimationPlayer animation;
    private Node2D sprites;
    private RayCast2D groundDetection;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.UP = new Vector2(0, -1);
        this.velocity = new Vector2();
        this.acceleration = new Vector2();

        this.animation = (AnimationPlayer)GetNode("animation_player");
        this.sprites = (Node2D)GetNode("sprites");
        this.groundDetection = (RayCast2D)GetNode("ground_detection");
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
            acceleration.x -= ACCEL;
        }
        else 
        {
            acceleration.x = 0;
        }

        if(acceleration.x == 0)
        {
            velocity.x *= FRICTION;
        }

        if(this.IsOnFloor())
        {
            if(Input.IsActionPressed("ui_up"))
            {
                velocity.y = JUMP_VEL;
            }
        }

        velocity += acceleration;

        velocity.x = this.clamp(velocity.x, -MAX_RUN_SPEED, MAX_RUN_SPEED);
        velocity.y = this.clamp(velocity.y, -MAX_FALL_SPEED, MAX_FALL_SPEED);

        velocity = MoveAndSlide(velocity, UP);

        if (velocity.Length() < 2)
        {
            velocity = new Vector2();
        }

        this.animate();
    }

    private void animate()
    {
        if(this.velocity.y > 0 && !this.IsOnFloor())
        {
            GD.Print(this.groundDetection.IsColliding());
            this.animation.Play("fall");
        }
        else if(this.velocity.y < 0 && !this.IsOnFloor())
        {
            this.animation.Play("jump");
        }
        else if(this.velocity.x != 0)
        {
            this.animation.Play("run");
        }
        else
        {
            this.animation.Play("idle");
        }

        if(this.velocity.x < 0)
        {
            this.sprites.Scale = new Vector2(-1, 1);
        }
        else if(this.velocity.x > 0)
        {
            this.sprites.Scale = new Vector2(1, 1);
        }

    }

    private float clamp(float val, float min, float max)
    {
        if(val < min)
        {
            return min;
        }
        else if(val > max)
        {
            return max;
        }
        else
        {
            return val;
        }
    }
}
