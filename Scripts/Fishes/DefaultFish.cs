using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class AlphaFish : AFish
    {
        public override string SpriteName => "alpha_fish";

        public override string ActionDescription => "None";


        string _name = "Basic fish";
        public override string Name { get => _name; set => _name = value; }

        public override void Ability(Node2D GameSceneRoot)
        {
        }
    }
}
