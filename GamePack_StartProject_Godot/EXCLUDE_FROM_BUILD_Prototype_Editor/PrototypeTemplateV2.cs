using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePackStartProjectGodot.EXCLUDE_FROM_BUILD_Prototype_Editor
{
    public abstract class AbstractEventClass
    {        
        public abstract void EventExecute(InputEvent @event);
    }
    public class ConcreteClassMoveButton : AbstractEventClass
    {
        PrototypeButtonAIV2 prototypeButtonAIV2;
        ButtonAIPrototypeModel buttonAIPrototypeModel;
        PrototypeModelV2 prototypeModelV2;
        public ConcreteClassMoveButton SetPrototypeButtonAIV2(PrototypeButtonAIV2 prototypeButtonAIV2, ButtonAIPrototypeModel buttonAIPrototypeModel, PrototypeModelV2 prototypeModelV2)
        {
            this.prototypeButtonAIV2 = prototypeButtonAIV2;
            this.buttonAIPrototypeModel = buttonAIPrototypeModel;
            this.prototypeModelV2 = prototypeModelV2;
            return this;
        }

        public override void EventExecute(InputEvent @event)
        {            
            if (@event is InputEventKey keyEvent && keyEvent.Pressed)
            {                
                if (keyEvent.Keycode == Key.Enter)
                {
                    //MoveButton();
                    this.prototypeButtonAIV2.DefineButtonAIPrototypeModel(this.buttonAIPrototypeModel, this.prototypeModelV2);
                    this.prototypeButtonAIV2.DefineButtonAIMovePosition(this.buttonAIPrototypeModel, this.prototypeModelV2);
                    this.prototypeButtonAIV2.MoveButton(this.buttonAIPrototypeModel, this.prototypeModelV2);
                }
            }
        }
    }
    public class ClientPrototypeV2
    {
        private AbstractEventClass abstractEventClass = null;
        public ClientPrototypeV2 SetAbstractEventClassBuilder(AbstractEventClass abstractEventClass)
        { 
            this.abstractEventClass = abstractEventClass;
            return this; 
        }
        public void ClientCode(InputEvent @event)
        {            
            abstractEventClass.EventExecute(@event);         
        }
    }
}
