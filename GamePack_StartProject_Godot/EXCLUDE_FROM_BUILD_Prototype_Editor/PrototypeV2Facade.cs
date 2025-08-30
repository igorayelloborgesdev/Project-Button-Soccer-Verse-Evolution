using GamePackStartProjectGodot.Scripts.Model;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePackStartProjectGodot.EXCLUDE_FROM_BUILD_Prototype_Editor
{
    public class PrototypeV2Facade
    {
        public void Init(Node node, PrototypeModelV2 prototypeModelV2)
        {
            new PrototypeFactoryV2().CreateObjects<MeshInstance3D>(node, prototypeModelV2.refName, out prototypeModelV2.objRef);
            new PrototypeFactoryV2().CreateObjects<RigidBody3D>(node, prototypeModelV2.ballName, out prototypeModelV2.ball);
            new PrototypeFactoryV2().CreateObjects<RigidBody3D>(node, prototypeModelV2.buttonPlayerName, out prototypeModelV2.button);
            new PrototypeFactoryV2().CreateObjects<MeshInstance3D>(node, prototypeModelV2.targetName, out prototypeModelV2.target);
            new PrototypeFactoryV2().CreateObjects<MeshInstance3D>(node, prototypeModelV2.targetHitName, out prototypeModelV2.targetHit);
        }        
        public void InitEvents(PrototypeButtonAIV2 prototypeButtonAIV2, ButtonAIPrototypeModel buttonAIPrototypeModel, PrototypeModelV2 prototypeModelV2)
        {            
            prototypeModelV2.concreteClassMoveButton = new ConcreteClassMoveButton().SetPrototypeButtonAIV2(prototypeButtonAIV2, buttonAIPrototypeModel, prototypeModelV2);
            prototypeModelV2.client = new ClientPrototypeV2().SetAbstractEventClassBuilder(prototypeModelV2.concreteClassMoveButton);
        }
    }
}
