using Godot;
using System;

public class Stage : Node2D
{
    public Seed Seed { get; set; }
    public Label GameOver { get; set;}

    private WinZone _zone;

    public override void _Ready()
    {
        this.Seed = GetNode<Seed>("Seed");
        this._zone = GetNode<WinZone>("WinZone");
        this._zone.Connect("GameWon", this, nameof(OnGameWon));
        this.Seed.Connect("SeedDied", this, nameof(OnSeedLanded));
        this.GameOver = GetNode<Label>("Camera2D/GameOver");
    }

    public override void _Process(float delta)
    {
        if(Input.IsActionPressed("reset"))
        {
            this.GetTree().ReloadCurrentScene();
        }
    }

    public void OnSeedLanded()
    {
        this.GameOver.Show();
    }

    public void OnGameWon(int clicks)
    {
        this.GameOver.Text = $"You Won in {clicks} clicks!";
        this.GameOver.Show();
    }
}
