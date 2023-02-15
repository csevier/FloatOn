using Godot;
using System;

public class game_won_dialog : Control
{
    public override void _Ready()
    {
        var signalMgr = GetNode("/root/SignalMgr");
        signalMgr.Call("register_subscriber", new object[] {this, "GameWon"});
        this.Hide();
        GD.Print("GameWonDialog ready");
    }

    public void _on_GameWon(int clicks){
        GD.Print("GameWonDialog game won");
        var label = GetNode<Label>("VBoxContainer/Label");
        label.Text = $"You Won in {clicks} clicks!";
        this.Show();
        GetTree().Paused = true;
    }

    public void _on_RetryBtn_pressed() {
        var transitionMgr = GetNode("/root/TransitionMgr");
        transitionMgr.Call("reload_current_scene");
    }

}
