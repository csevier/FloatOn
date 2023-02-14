using Godot;
using System;

public class Stage : Node2D
{
    private Seed _seed { get; set; }
    private WinZone _zone;

    public override void _Ready()
    {
        this._seed = GetNode<Seed>("StartZone/Seed");
        this._zone = GetNode<WinZone>("WinZone");
        this._zone.Connect("GameWon", this, nameof(OnGameWon));
    }

    public override void _Process(float delta)
    {
        if(Input.IsActionPressed("reset"))
        {
            this.GetTree().ReloadCurrentScene();
        }
    }

    public void OnGameWon(int clicks)
    {
        this._seed.OnGameWon(clicks);
    }

    public void OnGameLost()
    {
        this._seed.OnSeedDied();
    }
}
