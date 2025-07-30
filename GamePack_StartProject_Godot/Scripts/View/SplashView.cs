using Godot;
using System;

public partial class SplashView : Control
{
    #region Variables
    private SplashController splashController = new SplashController();
    #endregion
    #region Behavior
    public override void _Ready()
    {
        splashController.Init();
    }
    public override void _Process(double delta)
    {
        if (splashController.TimerCheck((float)delta))
            GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
    }
    #endregion  
}
