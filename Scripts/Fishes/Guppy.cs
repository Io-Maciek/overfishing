using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class Guppy : AFish
    {
        public override string SpriteName => "guppy";

        public override string ActionDescription => "Inverts other players controls.";


        string _name = "Guppy";
        public override string Name { get => _name; set => _name = value; }

        public override void Ability(Node2D GameSceneRoot)
        {
            Debug.WriteLine("===GUPPY ABILITY:");

            var you = this.GetYourself(GameSceneRoot);
            var others = this.GetOthers(GameSceneRoot);

            Debug.WriteLine("===END");
        }
    }
}
