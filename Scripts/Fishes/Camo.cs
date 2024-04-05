using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class Camo : AFish
    {
        public override string SpriteName => "camo";

        public override string ActionDescription => "Can't be hooked on fishing rod for X seconds.";


        string _name = "Camo";
        public override string Name { get => _name; set => _name = value; }

        public override ulong AbilityCooldown => throw new NotImplementedException();

        public override float AbilityUseTime => throw new NotImplementedException();

        public override void Ability(Node2D GameSceneRoot)
        {
        }
    }
}
