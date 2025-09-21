using GamePackStartProjectGodot.Scripts.Controller;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePackStartProjectGodot.EXCLUDE_FROM_BUILD_Prototype_Editor
{
    public partial class PrototypeViewV2 : Node
    {
        #region Variables
        private PrototypeControllerV2 prototypeControllerV2 = new PrototypeControllerV2();
        #endregion
        #region Behavior
        public override void _Ready()
        {
            prototypeControllerV2.Init(this);
        }
        public override void _Process(double delta)
        {
            GD.Print("TESTE");
        }
        public override void _Input(InputEvent @event)
        {            
            prototypeControllerV2.Input(@event);
        }
        #endregion
    }
}
