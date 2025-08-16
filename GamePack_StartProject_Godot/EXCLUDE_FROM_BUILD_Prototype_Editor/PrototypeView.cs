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
    private MeshInstance3D target = null;
    private MeshInstance3D targetHit = null;
    #endregion
    #region Variables
    private double distance = 0;
    private bool canStartSim = false;
    private double timer = 0.0;
    private double index = 5.0;
    private Vector3 newObjImp = new Vector3();
    private Vector3 objImp = new Vector3();
    #endregion
    #region Constants
    private const float targetBallDiff = 0.28f;
    private const float targetPlayerDiff = 1.529f;
    #endregion
    #region Behaviors
    public override void _Ready()
    {        
        InitObjects();
        
        var hypotenuseTargetBall = GetDistance(new Vector2(target.GlobalPosition.X, target.GlobalPosition.Z), new Vector2(ball.GlobalPosition.X, ball.GlobalPosition.Z)) + targetBallDiff + targetPlayerDiff;
        var angleRadiansTargetBall = GetAngleBetweenTwoObjects(new Vector3(target.GlobalPosition.X, target.GlobalPosition.Y, target.GlobalPosition.Z),
                                                        new Vector3(ball.GlobalPosition.X, ball.GlobalPosition.Y, ball.GlobalPosition.Z));
        float catOpoTargetBall = ((float)hypotenuseTargetBall * Mathf.Sin(angleRadiansTargetBall)) * 1.0f;
        float catAdjTargetBall = ((float)hypotenuseTargetBall * Mathf.Cos(angleRadiansTargetBall)) * 1.0f;
        targetHit.GlobalPosition = new Vector3(target.GlobalPosition.X + catAdjTargetBall, target.GlobalPosition.Y, target.GlobalPosition.Z + catOpoTargetBall);

        //--------------------------------------------
        var distTargetBall = GetDistance(new Vector2(button.GlobalPosition.X, button.GlobalPosition.Z), new Vector2(targetHit.GlobalPosition.X, targetHit.GlobalPosition.Z));
        var impTargetBall = XtoYCalculator.GetY(distTargetBall);//<-Forca botao
        var angleRadiansTargetBallFinal = GetAngleBetweenTwoObjects(new Vector3(button.GlobalPosition.X, button.GlobalPosition.Y, button.GlobalPosition.Z),
                                                                    new Vector3(targetHit.GlobalPosition.X, targetHit.GlobalPosition.Y, targetHit.GlobalPosition.Z));
        float hypotenuseFinal = (float)impTargetBall;
        float catOpoFinal = (hypotenuseFinal * Mathf.Sin(angleRadiansTargetBallFinal)) * 1.0f;
        float catAdjFinal = (hypotenuseFinal * Mathf.Cos(angleRadiansTargetBallFinal)) * 1.0f;
        objRef.GlobalPosition = new Vector3(button.GlobalPosition.X + catAdjFinal, objRef.GlobalPosition.Y, button.GlobalPosition.Z + catOpoFinal);
        newObjImp = objRef.GlobalPosition;
        objImp = new Vector3(button.GlobalPosition.X, objRef.GlobalPosition.Y, button.GlobalPosition.Z);

        var isNormalMove = DefineNormalPathMoveBasedOnAngleDifference(new Vector3(button.GlobalPosition.X, button.GlobalPosition.Y, button.GlobalPosition.Z),
                            new Vector3(target.GlobalPosition.X, target.GlobalPosition.Y, target.GlobalPosition.Z),
                            new Vector3(ball.GlobalPosition.X, ball.GlobalPosition.Y, ball.GlobalPosition.Z));//<-PAREI AQUI


        if (isNormalMove)
        {
            
        }
        else 
        { 
        
        }
        

        

        //GD.Print("---------------------");
        //GD.Print(GetQuadrant(new Vector2(button.GlobalPosition.X, button.GlobalPosition.Z), new Vector2(target.GlobalPosition.X, target.GlobalPosition.Z)));
        //GD.Print(GetQuadrant(new Vector2(button.GlobalPosition.X, button.GlobalPosition.Z), new Vector2(ball.GlobalPosition.X, ball.GlobalPosition.Z)));
        //GD.Print("@@@@@@@@@@@@@@@@@@@@@");
        //GD.Print(GetDistance(new Vector2(button.GlobalPosition.X, button.GlobalPosition.Z), new Vector2(target.GlobalPosition.X, target.GlobalPosition.Z)));
        //GD.Print(GetDistance(new Vector2(button.GlobalPosition.X, button.GlobalPosition.Z), new Vector2(ball.GlobalPosition.X, ball.GlobalPosition.Z)));
        //GD.Print("####################");
        //var angleButtonTarget = GetAngle90Abs(
        //        GetAngleBetweenTwoObjects(new Vector3(button.GlobalPosition.X, button.GlobalPosition.Y, button.GlobalPosition.Z),
        //        new Vector3(target.GlobalPosition.X, target.GlobalPosition.Y, target.GlobalPosition.Z))
        //        );
        //var angleButtonBall = GetAngle90Abs(
        //        GetAngleBetweenTwoObjects(new Vector3(button.GlobalPosition.X, button.GlobalPosition.Y, button.GlobalPosition.Z),
        //        new Vector3(ball.GlobalPosition.X, ball.GlobalPosition.Y, ball.GlobalPosition.Z))
        //        );
        //var angleDiff = angleButtonTarget - angleButtonBall;
        //GD.Print(angleButtonTarget);
        //GD.Print(angleButtonBall);
        //GD.Print(Mathf.Abs(angleDiff));
        //Max angle 25.0f

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
        target = this.GetNode<MeshInstance3D>("Target");
        targetHit = this.GetNode<MeshInstance3D>("TargetHit");
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
    private float GetAngleBetweenButtonAndBall()
    {
        if (ball is null)
            return 0.0f;
        return new Vector2(
                            ball.GlobalPosition.X,
                            ball.GlobalPosition.Z
                        ).AngleToPoint(new Vector2(
                            button.GlobalPosition.X,
                            button.GlobalPosition.Z
                        ));
    }
    private float GetAngleBetweenTwoObjects(Vector3 obj1, Vector3 obj2)
    {        
        return new Vector2(
                            obj1.X,
                            obj1.Z
                        ).AngleToPoint(new Vector2(
                            obj2.X,
                            obj2.Z
                        ));
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
        var impulse = (newObjImp - objImp);        
        impulse = new Vector3(impulse.X, button.GlobalPosition.Y, impulse.Z);
        button.ApplyImpulse(impulse);                
    }
    private int GetQuadrant(Vector2 buttonPos, Vector2 obj)
    {
        if (obj.X > buttonPos.X && obj.Y < buttonPos.Y)        
            return 1;        
        else if (obj.X < buttonPos.X && obj.Y < buttonPos.Y)        
            return 2;        
        else if (obj.X < buttonPos.X && obj.Y > buttonPos.Y)        
            return 3;        
        else if (obj.X > buttonPos.X && obj.Y > buttonPos.Y)        
            return 4;        
        return 0;
    }
    private float GetAngle90Abs(float angleRadians)
    {        
        return Mathf.Abs(Mathf.RadToDeg(angleRadians) - 90.0f);
    }

    private bool DefineNormalPathMoveBasedOnAngleDifference(Vector3 buttonObj, Vector3 targetObj, Vector3 ballObj)
    {
        var angleButtonTarget = GetAngle90Abs(GetAngleBetweenTwoObjects(buttonObj, targetObj));
        var angleButtonBall = GetAngle90Abs(GetAngleBetweenTwoObjects(buttonObj, ballObj));
        var angleDiff = Mathf.Abs(angleButtonTarget - angleButtonBall);

        GD.Print(angleDiff);
        GD.Print(angleDiff <= 25.0f);

        return angleDiff <= 25.0f;
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
