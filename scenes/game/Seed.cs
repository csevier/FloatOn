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
    [Export]
    public float PuffForce { get; set; } = 350;

    public override void _Ready()
    {
        this._seed = GetNode<Node2D>("Seed");
        this.StartingPosition = this.Position;
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
        Vector2 puffDirection = (mouseLocal.Normalized() * -1) * PuffForce; // flip it. and scale it by length
        return puffDirection;
    }

    public void StopAndDestroySeed()
    {
        this.SetDeferred("Mode", ModeEnum.Static);
        this.QueueFree();
    }
}
