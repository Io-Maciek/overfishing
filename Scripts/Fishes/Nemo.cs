using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class Nemo : AFish
    {
        public override string SpriteName => "nemo";

        public override string ActionDescription => "Hides other players for X seconds.";


        string _name = "Nemo";
        public override string Name { get => _name; set => _name = value; }

        public override void Ability(Node2D GameSceneRoot)
        {
        }
    }
}
