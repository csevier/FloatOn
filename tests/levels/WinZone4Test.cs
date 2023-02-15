using Godot;
using System;

public class WinZone4Test : WinZone
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var signalMgr = GetNode("/root/SignalMgr");
        signalMgr.Call("register_publisher", new object[] {this, nameof(GameWon)});
        GD.Print("WinZone4Test ready");
        base._Ready();
    }
}
