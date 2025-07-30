using Godot;
using System;

public partial class PrototypeView : Node
{
    #region Imports
    private MeshInstance3D objRef = null;
    private RigidBody3D ball = null;
    private RigidBody3D button = null;
    #endregion
    #region Variables
    private double distance = 0;
    #endregion
    #region Behaviors
    public override void _Ready()
    {        
        InitObjects();
    }
    public override void _Process(double delta)
    {
        GetAngleBetweenButtonAndBall();
    }
    public override void _Input(InputEvent @event)
    {
        MoveButton(@event);
    }
    #endregion
    #region Methods
    private void InitObjects()
    {
        objRef = this.GetNode<MeshInstance3D>("REF");
        ball = this.GetNode<RigidBody3D>("Ball");
        button = this.GetNode<RigidBody3D>("ButtonPlayer");

        GetDistance();
    }
    private void GetDistance()
    {
        distance = new Vector2(button.GlobalPosition.X, button.GlobalPosition.Z)
                        .DistanceTo(new Vector2(objRef.GlobalPosition.X, objRef.GlobalPosition.Z));
        GD.Print(distance);
    }
    private void GetAngleBetweenButtonAndBall()
    {
        if (ball is null)
            return;
        float angleRadians = new Vector2(
                            ball.GlobalPosition.X,
                            ball.GlobalPosition.Z
                        ).AngleToPoint(new Vector2(
                            button.GlobalPosition.X,
                            button.GlobalPosition.Z
                        ));

        

        //GD.Print("---------------------------------");
        //GD.Print(angleRadians);
        //float angleDegrees = angleRadians * (180 / MathF.PI);
        //GD.Print(angleDegrees);
    }
    private void MoveButton(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.Enter)
            {                
                var impulse = (objRef.GlobalPosition - button.GlobalPosition);
                impulse = new Vector3(impulse.X, button.GlobalPosition.Y, impulse.Z);
                button.ApplyImpulse(impulse);
            }
        }
    }

    #endregion
}
