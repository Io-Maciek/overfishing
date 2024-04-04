using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class Lanternfish : AFish
    {
        public override string SpriteName => "lanternfish";

        public override string ActionDescription => "Turns off the light!";


        string _name = "Lanternfish";
        public override string Name { get => _name; set => _name = value; }

        public override void Ability(Node2D GameSceneRoot)
        {
        }

        public override string SpriteFullPath()
        {
            return "res://Images/Fishies/lanternfish_full.png";
        }
    }
}
