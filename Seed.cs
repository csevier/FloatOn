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
            Vector2 mouseLocal = this.ToLocal(this.GetGlobalMousePosition());
            Vector2 puffDirection = (mouseLocal * -2); //fliped to other side of unit circle, this way you are pushing it not pulling it.
            if(!IsFloating)
            {
                this.Mode = ModeEnum.Rigid;
                this.IsFloating = true;
            }
            if(Math.Abs(puffDirection.Length()) > 350)
            {
                puffDirection = puffDirection.Normalized() * 350; // scale down to 350
            }
            this.ApplyImpulse(new Vector2(0,0), puffDirection);
        }
        if(this.Position.y > 600)
        {
            this.EmitSignal(nameof(SeedDied));
            this.StopAndDestroySeed();
        }
    }

    public void StopAndDestroySeed()
    {
        this.SetDeferred("Mode", ModeEnum.Static);
        this.QueueFree();
    }
}
