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
    }
}
