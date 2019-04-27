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

        if(inputEvent is InputEventMouseButton)
        {
            InputEventMouseButton mouseButtonEvent = inputEvent as InputEventMouseButton;

            if(mouseButtonEvent.IsActionPressed("camera_move_mouse"))
            {
                Input.SetMouseMode(Input.MouseMode.Captured);
            }

            if (mouseButtonEvent.IsActionReleased("camera_move_mouse"))
            {
                Input.SetMouseMode(Input.MouseMode.Visible);
            }
        }

        if(inputEvent is InputEventMouseMotion & Input.IsActionPressed("camera_move_mouse"))
        {
            InputEventMouseMotion mouseMotionEvent = inputEvent as InputEventMouseMotion;

            this.Translate(mouseMotionEvent.Relative);
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }
}
