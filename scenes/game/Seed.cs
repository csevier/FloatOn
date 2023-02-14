using Godot;
using System;

public class Seed : RigidBody2D
{
    public bool IsFloating { get; set; }
    private Vector2 StartingPosition;
    [Signal]
    public delegate void SeedDied();
    private Node2D _seed; 

    public int Clicks { get; private set; } = 0;
    private Line2D _aimLine;

    public override void _Ready()
    {
        this._seed = GetNode<Node2D>("Seed");
        this._aimLine = GetNode<Line2D>("AimLine");
        this.StartingPosition = this.Position;
        this._aimLine.Visible = true;
    }

    public override void _Process(float delta)
    {
        Vector2[] points = new Vector2[2];
        points[0] = new Vector2(0,0);
        points[1] = GetPuffVelocity() / 2;
        this._aimLine.Points = points;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("puff"))
        {
            Clicks+=1;
            if(!IsFloating)
            {
                this.Mode = ModeEnum.Character;
                this.IsFloating = true;
            }
            this.ApplyImpulse(new Vector2(0,0), GetPuffVelocity());
        }
        if(this.Position.y > 600)
        {
            this.EmitSignal(nameof(SeedDied));
            this.StopAndDestroySeed();
        }
    }

    private Vector2 GetPuffVelocity()
    {
        Vector2 mouseLocal = this.ToLocal(this.GetGlobalMousePosition());
        Vector2 puffDirection = (mouseLocal * -2);
        if(Math.Abs(puffDirection.Length()) > 350)
        {
            puffDirection = puffDirection.Normalized() * 350; // scale down to 350
        }
        return puffDirection;
    }

    public void StopAndDestroySeed()
    {
        this.SetDeferred("Mode", ModeEnum.Static);
        this.QueueFree();
    }
}
