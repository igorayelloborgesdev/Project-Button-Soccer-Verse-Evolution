using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePackStartProjectGodot.EXCLUDE_FROM_BUILD_Prototype_Editor
{
    public class ButtonAIPrototypeModel
    {
        public bool isNormalPathMoveBasedOnAngleDifference { get; set; }
        public bool isSameQuadrant { get; set; }
        public bool isTargetDistanceGreaterThanBallDistance { get; set; }
        public bool isNormalMove { get; set; }
        public int targetQuadrant { get; set; }
        public int ballQuadrant { get; set; }
        public double distButtonTarget { get; set; }
        public double distButtonBall { get; set; }
        public float angleButtonTarget { get; set; }
        public float angleButtonBall { get; set; }
        public float angleDiff { get; set; }
        public Vector3 newObjImp { get; set; }
        public Vector3 objImp { get; set; }
        #region Constants
        public const float angleDiffMin = 25.0f;
        public float GetAngleDiffMin
        { 
            get { return angleDiffMin; }
        }        
        public const float targetBallDiff = 0.28f;
        public float GetTargetBallDiff
        {
            get { return targetBallDiff; }
        }
        public const float targetPlayerDiff = 1.529f;
        public float GetTargetPlayerDiff
        {
            get { return targetPlayerDiff; }
        }
        #endregion
    }
}
