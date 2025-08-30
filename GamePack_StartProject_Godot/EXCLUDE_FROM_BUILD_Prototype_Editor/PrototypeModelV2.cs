using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePackStartProjectGodot.EXCLUDE_FROM_BUILD_Prototype_Editor
{
    public class PrototypeModelV2
    {
        #region Node Name
        public string refName { get; set; } = "REF";
        public string ballName { get; set; } = "Ball";
        public string buttonPlayerName { get; set; } = "ButtonPlayer";
        public string targetName { get; set; } = "Target";
        public string targetHitName { get; set; } = "TargetHit";
        #endregion                                
        #region Variables
        public MeshInstance3D objRef;
        public RigidBody3D ball;
        public RigidBody3D button;
        public MeshInstance3D target;
        public MeshInstance3D targetHit;
        #endregion
        #region Events
        public ClientPrototypeV2 client;
        public ConcreteClassMoveButton concreteClassMoveButton;
        #endregion
    }
}
