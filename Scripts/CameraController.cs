/* 
 * *********************************************************************************************************
 * Project: BArc
 * Author: Lucas Bulloni & Malik Fleury
 * Date: 27.04.2019
 * Description: Controller for the camera
 * *********************************************************************************************************
 */

using Godot;
using System;

public class CameraController : Camera2D
{
    private readonly float SPEED = 50.0f;
    private readonly float ZOOM_SPEED = 0.5f;
    private readonly Vector2 MAX_ZOOM_IN = new Vector2(1.0f, 1.0f);
    private readonly Vector2 MAX_ZOOM_OUT = new Vector2(10.0f, 10.0f);
    private readonly Vector2 DEFAULT_ZOOM = new Vector2(5.0f, 5.0f);

    /// <summary>
    /// Initialise the node when the game engine is ready
    /// </summary>
    public override void _Ready()
    {
        this.Zoom = DEFAULT_ZOOM;
    }

    /// <summary>
    /// Move the camera depending of the key pressed or the motion detected
    /// </summary>
    /// <param name="inputEvent"></param>
    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);

        // Zoom when wheel is used
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

        // Detect if the right button is just pressed or just released
        // and start camera movement
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

        // If motion is detected and the right button is pressed, the camera is moved
        if(inputEvent is InputEventMouseMotion & Input.IsActionPressed("camera_move_mouse"))
        {
            InputEventMouseMotion mouseMotionEvent = inputEvent as InputEventMouseMotion;

            this.Translate(mouseMotionEvent.Relative);
        }
    }
}
