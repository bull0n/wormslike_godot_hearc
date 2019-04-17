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

    enum State { Idle, Running, Shooting, Falling, Jumping }
    enum SelectableWeapon { Bazooka, Grenade, Rifle }


    const int GRAVITY = 980;
    const int JUMP_VEL = -150000;
    const int ACCEL = 1000;
    const float FRICTION = 0.4f;
    private State state;
    private Vector2 UP; 
    private Vector2 velocity;
    private Vector2 acceleration;
    private AnimationPlayer animation;
    private Node2D sprites;
    private RayCast2D groundDetection;
    private Sprite rightArm;
    private Weapon weapon;
    private SelectableWeapon selectedWeapon;
    private int startTime;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.UP = new Vector2(0, -1);
        this.velocity = new Vector2();
        this.acceleration = new Vector2();

        this.animation = (AnimationPlayer)GetNode("animation_player");
        this.sprites = (Node2D)GetNode("sprites");
        this.groundDetection = (RayCast2D)GetNode("ground_detection");
        this.rightArm = (Sprite)this.sprites.GetNode("arm_right");
        this.weapon = null;
        this.selectedWeapon = SelectableWeapon.Rifle;
    }

    public override void _PhysicsProcess(float delta)
    {
        acceleration.y = GRAVITY;
        
        this.Move();

        if(acceleration.x == 0)
        {
            velocity.x *= FRICTION;
        }

        this.Jump();
        this.ComputeVelocityAndMove();
        this.setMovingState();
        this.Shoot();
        this.animate();
    }

    private void animate()
    {
        if(this.state == State.Falling)
        {
            this.animation.Play("fall");
        }
        else if(this.state == State.Jumping)
        {
            //this.animation.Play("jump");
        }
        else if(this.state == State.Running)
        {
            this.animation.Play("run");
        }
        else if(this.state == State.Shooting)
        {
            this.animation.Stop();
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

    private float Clamp(float val, float min, float max)
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

    private void Move()
    {
        if(this.CanRunOrJump())
        {   
            this.state = State.Running;
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
                this.state = State.Idle;
            }
        }
    }

    private void Jump()
    {
        if(this.IsOnFloor())
        {
            if(Input.IsActionPressed("ui_up"))
            {
                velocity.y = JUMP_VEL;
            }
        }
    }

    private void Shoot()
    {
        if(Input.IsActionJustReleased("shoot"))
        {
            int elapsedTime = OS.GetSystemTimeMsecs() - startTime;
            this.weapon.Shoot(elapsedTime);
        }

        if(Input.IsActionJustPressed("shoot"))
        {
            this.startTime = OS.GetSystemTimeMsecs();

            this.state = State.Shooting;
            this.InstantiateWeapon();
        }

        if(Input.IsActionPressed("shoot") && this.CanShoot())
        {
            Vector2 position = GetGlobalMousePosition();

            this.state = State.Shooting;
            this.rightArm.SetRotation(this.rightArm.GetGlobalPosition().AngleToPoint(GetGlobalMousePosition()) + 90);
        }
    }

    private void ComputeVelocityAndMove()
    {
        velocity += acceleration;
        velocity.x = this.Clamp(velocity.x, -MAX_RUN_SPEED, MAX_RUN_SPEED);
        velocity.y = this.Clamp(velocity.y, -MAX_FALL_SPEED, MAX_FALL_SPEED);
        velocity = MoveAndSlide(velocity, UP);

        if (velocity.Length() < 2)
        {
            velocity = new Vector2();
        }
    }

    private void InstantiateWeapon() 
    {       
        if(this.weapon == null)
        {
            PackedScene weapon = null;
            
            if(this.selectedWeapon == SelectableWeapon.Bazooka)
            {
                weapon = (PackedScene)ResourceLoader.Load("res://Scenes/Weapons/RocketLauncher.tscn");
            }
            else if(this.selectedWeapon == SelectableWeapon.Grenade)
            {
                weapon = (PackedScene)ResourceLoader.Load("res://Scenes/Weapons/HandGrenade.tscn");
            }
            else if(this.selectedWeapon == SelectableWeapon.Rifle)
            {
                weapon = (PackedScene)ResourceLoader.Load("res://Scenes/Weapons/Rifle.tscn");
            }
            
            this.weapon = (Weapon)weapon.Instance();
            this.rightArm.AddChild(this.weapon);
            this.weapon.SetTransform(((Node2D)rightArm.GetNode("handle_weapon")).GetTransform());
        }

            //this.rightArm.RemoveChild(this.weapon);
    }

    private void setMovingState()
    {
        if(this.velocity.x != 0 && this.IsOnFloor())
        {
            this.state = State.Running;
        }
        else if(this.velocity.y < 0 && !this.IsOnFloor())
        {
            this.state = State.Jumping;
        }
        else if(this.velocity.y > 0 && !this.IsOnFloor())
        {
            this.state = State.Falling;
        }
        else 
        {
            this.state = State.Idle;
        }
    }

    private bool CanRunOrJump()
    {
        return !(this.state == State.Shooting);
    }

    private bool CanShoot()
    {
        return this.state == State.Idle;
    }
}
