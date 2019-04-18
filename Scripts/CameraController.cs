using Godot;
using System;

public class CameraController : Camera2D
{
    private readonly float SPEED = 50.0f;
    private readonly float ZOOM_SPEED = 0.5f;
    private readonly Vector2 MAX_ZOOM_IN = new Vector2(1.0f, 1.0f);
    private readonly Vector2 MAX_ZOOM_OUT = new Vector2(10.0f, 10.0f);
    private readonly Vector2 DEFAULT_ZOOM = new Vector2(5.0f, 5.0f);

    public override void _Ready()
    {
        this.Zoom = DEFAULT_ZOOM;
    }

    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);

        if(inputEvent is InputEventMouseButton)
        {
            InputEventMouseButton mouseButtonEvent = inputEvent as InputEventMouseButton;

            if (mouseButtonEvent.IsAction("camera_zoom_out"))
            {
                if (this.Zoom.x < MAX_ZOOM_OUT.x)
                {
                    this.Zoom = this.Zoom + Vector2.One * ZOOM_SPEED;
                }
            }
            else if (mouseButtonEvent.IsAction("camera_zoom_in"))
            {
                if (this.Zoom.x > MAX_ZOOM_IN.x)
                {
                    this.Zoom = this.Zoom - Vector2.One * ZOOM_SPEED;
                }
            }
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (Input.IsActionPressed("camera_left"))
        {
            this.Translate(Vector2.Left * SPEED);
        }

        if (Input.IsActionPressed("camera_right"))
        {
            this.Translate(Vector2.Right * SPEED);
        }

        if (Input.IsActionPressed("camera_up"))
        {
            this.Translate(Vector2.Up * SPEED);
        }

        if (Input.IsActionPressed("camera_down"))
        {
            this.Translate(Vector2.Down * SPEED);
        }
    }
}
