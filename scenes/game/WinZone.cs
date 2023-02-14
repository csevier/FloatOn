using Godot;
using System;

public class WinZone : Sprite
{
    [Signal]
    public delegate void GameWon(int clicks);
    private Area2D zone;
    public override void _Ready()
    {
        zone = this.GetNode<Area2D>("Area2D");
        zone.Connect("body_entered", this, nameof(Won));
    }

    private void Won(Node body)
    {
        Seed seed = (Seed)body;
        this.EmitSignal(nameof(GameWon), seed.Clicks);
        seed.StopAndDestroySeed();
    }
}
