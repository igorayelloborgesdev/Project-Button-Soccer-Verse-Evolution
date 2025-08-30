using GamePackStartProjectGodot.Scripts.Facade;
using GamePackStartProjectGodot.Scripts.Model;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePackStartProjectGodot.EXCLUDE_FROM_BUILD_Prototype_Editor
{
    public class PrototypeControllerV2
    {
        private PrototypeModelV2 prototypeModelV2 = new PrototypeModelV2();
        private ButtonAIPrototypeModel buttonAIPrototypeModel = new ButtonAIPrototypeModel();
        private PrototypeButtonAIV2 prototypeButtonAIV2 = new PrototypeButtonAIV2();
        private PrototypeV2Facade prototypeV2Facade = new PrototypeV2Facade();        
        public void Init(Node node)
        {
            prototypeV2Facade.Init(node, prototypeModelV2);
            prototypeV2Facade.InitEvents(prototypeButtonAIV2, buttonAIPrototypeModel, prototypeModelV2);
        }
        public void Update(double delta)
        {
            
        }
        public void Input(InputEvent @event)
        {
            prototypeModelV2.client.ClientCode(@event);            
            //MoveButtonEvent(@event);            
        }
    }
}
