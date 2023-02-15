using Godot;
using System;

public class test_level_base : Node2D
{

    [Signal]
    public delegate void GameOver();

    public override void _Ready()
    {
        var signalMgr = GetNode("/root/SignalMgr");
        signalMgr.Call("register_publisher", new object[] {this, nameof(GameOver)});
        GD.Print("TestLevelBase ready");

        // var camera2D = (Camera2D) FindNode("Camera2D");
        // if(camera2D == null) {
        //     GD.PrintErr("TestLevelBase: could not get camera2d.");
        //     return;
        // }
        // GD.Print(camera2D.GetPath());
        // GD.Print("TestLevelBase: 1");
        // //res://addons/godot_helper_pack/core/2d/camera/behavior/camera_limiter.tscn
        // //res://addons/godot_helper_pack/core/2d/camera/behavior/camera_limiter.gd
        // var limiterScene = (PackedScene)GD.Load("res://addons/godot_helper_pack/core/2d/camera/behavior/camera_limiter.tscn");
        // GD.Print("TestLevelBase: 2");
        // var limiter = (Node) limiterScene.Instance();
        // GD.Print("TestLevelBase: 3");
        // camera2D.AddChild(limiter);
        // GD.Print("TestLevelBase: 4");
        // var refRect = GetNode<ReferenceRect>("ReferenceRect");
        // if(refRect == null) {
        //     GD.PrintErr("TestLevelBase: ref rect not found");
        //     return;
        // }
        // GD.Print(refRect.GetPath());
        // GD.Print("TestLevelBase: 5");
        // limiter.Call("limit_camerax", refRect);
        // limiter.Set("limit_reference_rectx", refRect);
        // GD.Print("TestLevelBase: 6");


    }

    public void _on_Area2D_body_entered(Node body) {
        if(body.Name != "Seed") {
            return;
        }
        GD.Print("TestLevelBase GameOver");
        this.EmitSignal(nameof(GameOver));
    }

}
