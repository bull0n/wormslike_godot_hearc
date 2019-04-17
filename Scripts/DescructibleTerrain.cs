using Godot;
using System;

public class DescructibleTerrain : Area2D
{

    [Export]
    private int MIN_SIZE = 32;
    private int size;

    DescructibleTerrain() 
    {
        this.size = 256;
    }

    public override void _Ready()
    {
        Connect("body_entered", this, "_on_Node2D_area_entered");
        //this.Split();
    }

    public void _on_Node2D_area_entered(PhysicsBody2D area)
    {
        if(area.GetType() != typeof(Area2D))
        {
            this.Split();
        }
    }

    public void Split()
    {
        if(size <= 16)
        {
            this.QueueFree();
		    return;
        }

        this.size /= 2;

        for(int i = -1; i <= 1; i += 2)
        {
            for(int j = -1; j <= 1; j += 2)
            {
                var newNode = this.MakeNewNode();
                newNode.size = this.size;
                float position = newNode.size / 2.0f;
                
                Vector2 relativePosition = new Vector2(position * i, position * j);
                newNode.SetPosition(GetPosition() + relativePosition);
            }
        }
        
        this.QueueFree();
    }

    public DescructibleTerrain MakeNewNode()
    {

        DescructibleTerrain newNode = Duplicate() as DescructibleTerrain;
        GetParent().CallDeferred("add_child", newNode);
        newNode.SetScale(GetScale()/2.0f);
        return newNode;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
