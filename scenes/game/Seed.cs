using Godot;
using System;

public class Seed : RigidBody2D
{
    public bool IsFloating { get; set; }
    private Vector2 StartingPosition;
    [Signal]
    public delegate void SeedDied();
    public int Clicks { get; private set; } = 0;
    [Export]
    public float PuffForce { get; set; } = 350;
    private Label _gameOverLabel;

    public override void _Ready()
    {
        this.StartingPosition = this.Position;
        this._gameOverLabel = GetNode<Label>("Camera2D/GameOver");
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
    }

    private Vector2 GetPuffVelocity()
    {
        Vector2 mouseLocal = this.ToLocal(this.GetGlobalMousePosition());
        Vector2 puffDirection = (mouseLocal.Normalized() * -1) * PuffForce; // flip it. and scale it by length
        return puffDirection;
    }

    public void StopSeed()
    {
        this.SetPhysicsProcess(false);
        this.Sleeping = true;
    }
    public void OnSeedDied()
    {
        this.StopSeed();
        this._gameOverLabel.Show();
    }

    public void OnGameWon(int clicks)
    {
        this.StopSeed();
        this._gameOverLabel.Text = $"You Won in {clicks} clicks!";
        this._gameOverLabel.Show();
    }
}
