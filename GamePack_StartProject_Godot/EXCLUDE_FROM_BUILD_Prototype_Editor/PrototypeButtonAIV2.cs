using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePackStartProjectGodot.EXCLUDE_FROM_BUILD_Prototype_Editor
{
    public class PrototypeButtonAIV2
    {
        public void DefineButtonAIPrototypeModel(ButtonAIPrototypeModel buttonAIPrototypeModel, PrototypeModelV2 prototypeModelV2)
        {
            var isNormalPathMoveBasedOnAngleDifference = DefineNormalPathMoveBasedOnAngleDifference(
                                new Vector3(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Y, prototypeModelV2.button.GlobalPosition.Z),
                                new Vector3(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.target.GlobalPosition.Z),
                                new Vector3(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z),
                                buttonAIPrototypeModel);
            buttonAIPrototypeModel.isNormalPathMoveBasedOnAngleDifference = isNormalPathMoveBasedOnAngleDifference;

            var targetQuadrant = GetQuadrant(new Vector2(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Z), 
                                            new Vector2(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Z));
            var ballQuadrant = GetQuadrant(new Vector2(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Z), 
                                            new Vector2(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Z));
            buttonAIPrototypeModel.targetQuadrant = targetQuadrant;
            buttonAIPrototypeModel.ballQuadrant = ballQuadrant;
            buttonAIPrototypeModel.isSameQuadrant = targetQuadrant == ballQuadrant;

            var distButtonTarget = GetDistance(new Vector2(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Z), 
                                                new Vector2(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Z));
            var distButtonBall = GetDistance(new Vector2(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Z), 
                                            new Vector2(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Z));
            buttonAIPrototypeModel.distButtonTarget = distButtonTarget;
            buttonAIPrototypeModel.distButtonBall = distButtonBall;
            buttonAIPrototypeModel.isTargetDistanceGreaterThanBallDistance = distButtonTarget > distButtonBall;

            var angleButtonTarget = GetAngle90Abs(
                    GetAngleBetweenTwoObjects(
                        new Vector3(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Y, prototypeModelV2.button.GlobalPosition.Z),
                        new Vector3(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.target.GlobalPosition.Z))
                    );
            var angleButtonBall = GetAngle90Abs(
                    GetAngleBetweenTwoObjects(
                        new Vector3(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Y, prototypeModelV2.button.GlobalPosition.Z),
                        new Vector3(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z))
                    );
            var angleDiff = angleButtonTarget - angleButtonBall;
            buttonAIPrototypeModel.angleButtonTarget = angleButtonTarget;
            buttonAIPrototypeModel.angleButtonBall = angleButtonBall;
            buttonAIPrototypeModel.angleDiff = angleDiff;

            buttonAIPrototypeModel.isNormalMove = (buttonAIPrototypeModel.isNormalPathMoveBasedOnAngleDifference &&
                                                   buttonAIPrototypeModel.isSameQuadrant &&
                                                   buttonAIPrototypeModel.isTargetDistanceGreaterThanBallDistance);            
        }
        public void DefineButtonAIMovePosition(ButtonAIPrototypeModel buttonAIPrototypeModel, PrototypeModelV2 prototypeModelV2)
        {
            if (buttonAIPrototypeModel.isNormalMove)
            {
                var hypotenuseTargetBall = GetDistance(new Vector2(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Z), 
                    new Vector2(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Z)) 
                    + buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff;
                var angleRadiansTargetBall = GetAngleBetweenTwoObjects(new Vector3(prototypeModelV2.target.GlobalPosition.X, 
                    prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.target.GlobalPosition.Z),
                                                                new Vector3(prototypeModelV2.ball.GlobalPosition.X, 
                                                                prototypeModelV2.ball.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z));
                float catOpoTargetBall = ((float)hypotenuseTargetBall * Mathf.Sin(angleRadiansTargetBall)) * 1.0f;
                float catAdjTargetBall = ((float)hypotenuseTargetBall * Mathf.Cos(angleRadiansTargetBall)) * 1.0f;
                prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.target.GlobalPosition.X + catAdjTargetBall, 
                    prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.target.GlobalPosition.Z + catOpoTargetBall);

                var hypotenusePower = GetDistance(new Vector2(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Z), 
                    new Vector2(prototypeModelV2.targetHit.GlobalPosition.X, prototypeModelV2.targetHit.GlobalPosition.Z)) +
                                      (GetDistance(new Vector2(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Z), 
                                      new Vector2(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Z)) * 0.5f);
                var impTargetBall = XtoYCalculator.GetY(hypotenusePower);
                var angleRadiansTargetBallFinal = GetAngleBetweenTwoObjects(new Vector3(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Y, 
                    prototypeModelV2.button.GlobalPosition.Z), new Vector3(prototypeModelV2.targetHit.GlobalPosition.X, 
                    prototypeModelV2.targetHit.GlobalPosition.Y, prototypeModelV2.targetHit.GlobalPosition.Z));
                float hypotenuseFinal = (float)impTargetBall;
                float catOpoFinal = (hypotenuseFinal * Mathf.Sin(angleRadiansTargetBallFinal)) * 1.0f;
                float catAdjFinal = (hypotenuseFinal * Mathf.Cos(angleRadiansTargetBallFinal)) * 1.0f;
                prototypeModelV2.objRef.GlobalPosition = new Vector3(prototypeModelV2.button.GlobalPosition.X + catAdjFinal, prototypeModelV2.objRef.GlobalPosition.Y, 
                    prototypeModelV2.button.GlobalPosition.Z + catOpoFinal);
                buttonAIPrototypeModel.newObjImp = prototypeModelV2.objRef.GlobalPosition;
                buttonAIPrototypeModel.objImp = new Vector3(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.objRef.GlobalPosition.Y, prototypeModelV2.button.GlobalPosition.Z);
            }
            else
            {
                GetQuadBallPosition(buttonAIPrototypeModel, prototypeModelV2);
                var hypotenuseTargetBall = GetDistance(new Vector2(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Z), 
                    new Vector2(prototypeModelV2.targetHit.GlobalPosition.X, prototypeModelV2.targetHit.GlobalPosition.Z));
                var angleRadiansTargetBall = GetAngleBetweenTwoObjects(new Vector3(prototypeModelV2.button.GlobalPosition.X, 
                    prototypeModelV2.button.GlobalPosition.Y, prototypeModelV2.button.GlobalPosition.Z),
                                                                new Vector3(prototypeModelV2.targetHit.GlobalPosition.X, 
                                                                prototypeModelV2.targetHit.GlobalPosition.Y, prototypeModelV2.targetHit.GlobalPosition.Z));
                var distTargetBall = GetDistance(new Vector2(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.button.GlobalPosition.Z), 
                    new Vector2(prototypeModelV2.targetHit.GlobalPosition.X, prototypeModelV2.targetHit.GlobalPosition.Z));
                var impTargetBall = XtoYCalculator.GetY(distTargetBall);
                float hypotenuseFinal = (float)impTargetBall;
                float catOpoFinal = (hypotenuseFinal * Mathf.Sin(angleRadiansTargetBall)) * 1.0f;
                float catAdjFinal = (hypotenuseFinal * Mathf.Cos(angleRadiansTargetBall)) * 1.0f;
                prototypeModelV2.objRef.GlobalPosition = new Vector3(prototypeModelV2.button.GlobalPosition.X + catAdjFinal, 
                    prototypeModelV2.objRef.GlobalPosition.Y, prototypeModelV2.button.GlobalPosition.Z + catOpoFinal);
                buttonAIPrototypeModel.newObjImp = prototypeModelV2.objRef.GlobalPosition;
                buttonAIPrototypeModel.objImp = new Vector3(prototypeModelV2.button.GlobalPosition.X, prototypeModelV2.objRef.GlobalPosition.Y, prototypeModelV2.button.GlobalPosition.Z);
            }            
        }
        private void GetQuadBallPosition(ButtonAIPrototypeModel buttonAIPrototypeModel, PrototypeModelV2 prototypeModelV2)
        {
            int ballQuad = 0;
            if (buttonAIPrototypeModel.ballQuadrant == 1)
            {
                ballQuad = GetQuadrant(new Vector2(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Z), 
                    new Vector2(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Z));
                if (ballQuad == 1 || ballQuad == 4)
                {
                    prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.ball.GlobalPosition.X 
                        - (buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff), 
                        prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z);
                }
                else if (ballQuad == 2 || ballQuad == 3)
                {
                    prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.ball.GlobalPosition.X,
                        prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z + 
                        (buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff));
                }
            }
            else if (buttonAIPrototypeModel.ballQuadrant == 2)
            {
                ballQuad = GetQuadrant(new Vector2(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Z), 
                    new Vector2(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Z));
                if (ballQuad == 1 || ballQuad == 4)
                {
                    prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Y, 
                        prototypeModelV2.ball.GlobalPosition.Z + (buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff));
                }
                else if (ballQuad == 2 || ballQuad == 3)
                {
                    prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.ball.GlobalPosition.X + 
                        (buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff), 
                        prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z);
                }
            }
            else if (buttonAIPrototypeModel.ballQuadrant == 3)
            {
                ballQuad = GetQuadrant(new Vector2(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Z), 
                    new Vector2(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Z));
                if (ballQuad == 1 || ballQuad == 4)
                {
                    prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Y, 
                        prototypeModelV2.ball.GlobalPosition.Z - (buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff));
                }
                else if (ballQuad == 2 || ballQuad == 3)
                {
                    prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.ball.GlobalPosition.X 
                        + (buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff), 
                        prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z);
                }
            }
            else if (buttonAIPrototypeModel.ballQuadrant == 4)
            {
                ballQuad = GetQuadrant(new Vector2(prototypeModelV2.ball.GlobalPosition.X, prototypeModelV2.ball.GlobalPosition.Z), 
                    new Vector2(prototypeModelV2.target.GlobalPosition.X, prototypeModelV2.target.GlobalPosition.Z));
                if (ballQuad == 1 || ballQuad == 4)
                {
                    prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.ball.GlobalPosition.X 
                        - (buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff), 
                        prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z);
                }
                else if (ballQuad == 2 || ballQuad == 3)
                {
                    prototypeModelV2.targetHit.GlobalPosition = new Vector3(prototypeModelV2.ball.GlobalPosition.X, 
                        prototypeModelV2.target.GlobalPosition.Y, prototypeModelV2.ball.GlobalPosition.Z 
                        - (buttonAIPrototypeModel.GetTargetBallDiff + buttonAIPrototypeModel.GetTargetPlayerDiff));
                }
            }
        }
        public void MoveButton(ButtonAIPrototypeModel buttonAIPrototypeModel, PrototypeModelV2 prototypeModelV2)
        {            
            var impulse = (buttonAIPrototypeModel.newObjImp - buttonAIPrototypeModel.objImp);
            impulse = new Vector3(impulse.X, prototypeModelV2.button.GlobalPosition.Y, impulse.Z);
            prototypeModelV2.button.ApplyImpulse(impulse);
        }

        private bool DefineNormalPathMoveBasedOnAngleDifference(Vector3 buttonObj, Vector3 targetObj, Vector3 ballObj, ButtonAIPrototypeModel buttonAIPrototypeModel)
        {
            var angleButtonTarget = GetAngle90Abs(GetAngleBetweenTwoObjects(buttonObj, targetObj));
            var angleButtonBall = GetAngle90Abs(GetAngleBetweenTwoObjects(buttonObj, ballObj));
            var angleDiff = Mathf.Abs(angleButtonTarget - angleButtonBall);
            return angleDiff <= buttonAIPrototypeModel.GetAngleDiffMin;
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
        private double GetDistance(Vector2 obj1, Vector2 obj2)
        {
            return new Vector2(obj1.X, obj1.Y).DistanceTo(new Vector2(obj2.X, obj2.Y));
        }
        private float GetAngle90Abs(float angleRadians)
        {
            return Mathf.Abs(Mathf.RadToDeg(angleRadians) - 90.0f);
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
    }
}
