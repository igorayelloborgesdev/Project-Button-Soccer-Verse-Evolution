using GamePackStartProjectGodot.EXCLUDE_FROM_BUILD_Prototype_Editor;
using Godot;
using System;
using System.Reflection;

public partial class PrototypeView : Node
{
    #region Imports
    private MeshInstance3D objRef = null;
    private RigidBody3D ball = null;
    private RigidBody3D button = null;
    #endregion
    #region Variables
    private double distance = 0;
    private bool canStartSim = false;
    private double timer = 0.0;
    private double index = 5.0;
    private Vector3 newObjImp = new Vector3();
    #endregion
    #region Behaviors
    public override void _Ready()
    {        
        InitObjects();        
        var dist = GetDistance(new Vector2(button.GlobalPosition.X, button.GlobalPosition.Z), new Vector2(ball.GlobalPosition.X, ball.GlobalPosition.Z));
        var imp = XtoYCalculator.GetY(dist);        
        objRef.GlobalPosition = new Vector3(ball.GlobalPosition.X, objRef.GlobalPosition.Y, ball.GlobalPosition.Z);
        newObjImp = new Vector3(objRef.GlobalPosition.X , objRef.GlobalPosition.Y, objRef.GlobalPosition.Z + ((float)GetButtonMoveOrientation() + (float)imp));

        GD.Print(GetButtonMoveOrientation());
        GD.Print(dist);
        GD.Print(imp);
    }
    public override void _Process(double delta)
    {
        //GetAngleBetweenButtonAndBall();
        //GetDistance();
        //StartMovingSim();
        //ResetButton();
        //UpdateTimer(delta);        
    }
    public override void _Input(InputEvent @event)
    {
        MoveButtonEvent(@event);
        //StartMovingSim(@event);
    }
    #endregion
    #region Methods
    private void InitObjects()
    {
        objRef = this.GetNode<MeshInstance3D>("REF");
        ball = this.GetNode<RigidBody3D>("Ball");
        button = this.GetNode<RigidBody3D>("ButtonPlayer");        
    }
    private double GetButtonMoveOrientation()
    {
        if (button.GlobalPosition.Z > objRef.GlobalPosition.Z)
        {
            return -1.0;
        }
        else if (button.GlobalPosition.Z < objRef.GlobalPosition.Z)
        {
            return 1.0;
        }
        return 0.0;
    }
    private void UpdateTimer(double delta)
    {
        if (canStartSim)
        {
            timer += delta;
        }
    }
    private double GetDistance(Vector2 obj1, Vector2 obj2)
    {
        return new Vector2(obj1.X, obj1.Y).DistanceTo(new Vector2(obj2.X, obj2.Y));        
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
    
    private void ResetButton()
    {
        if (button.LinearVelocity.Length() == 0 && canStartSim && timer > 1.0)
        {
            GD.Print((button.GlobalPosition.Z * -1.0).ToString() + " | " + (index - 0.1).ToString());
            canStartSim = false;
            timer = 0.0;
            button.GlobalPosition = new Vector3(0.0f, button.GlobalPosition.Y, 0.0f);
        }        
    }
    private void MoveButton()
    {
        canStartSim = true;        
        var impulse = (objRef.GlobalPosition - newObjImp);
        impulse = new Vector3(impulse.X, button.GlobalPosition.Y, impulse.Z);
        button.ApplyImpulse(impulse);
    }
    #endregion
    #region Events
    private void MoveButtonEvent(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.Enter)
            {
                MoveButton();
            }
        }
    }
    #endregion
    #region Simulate
    private void StartMovingSim(InputEvent @event)
    {
        if (!canStartSim)
        {
            if (@event is InputEventKey keyEvent && keyEvent.Pressed)
            {
                if (keyEvent.Keycode == Key.Enter)
                {
                    SimulateButton(index);
                    index += 0.1;
                }
            }
        }
    }
    private void SimulateButton(double imp)
    {
        canStartSim = true;
        SetobjRefPosition((float)imp);
        var impulse = (objRef.GlobalPosition - button.GlobalPosition);
        impulse = new Vector3(impulse.X, button.GlobalPosition.Y, impulse.Z);
        button.ApplyImpulse(impulse);
    }
    private void SetobjRefPosition(float impulse)
    {
        objRef.GlobalPosition = new Vector3(button.GlobalPosition.X, objRef.GlobalPosition.Y, button.GlobalPosition.Z - impulse);
    }
    #endregion
}
