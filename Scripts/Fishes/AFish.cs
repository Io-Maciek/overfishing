using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public abstract class AFish
    {
        public abstract string Name { get; set; }
        public abstract string SpriteName { get;  }
        public abstract string ActionDescription { get;  }

        public abstract void Ability(Node2D GameSceneRoot);

        public virtual string SpriteFullPath()
        {
            string sprite_name = $"{SpriteName}.png";
            return $"res://Images/Fishies/{sprite_name}";
        }

        public virtual string SceneFullPath()
        {
            string scene_name = $"{SpriteName}.tscn";
            return $"res://Scenes/Prefabs/Fishes/{scene_name}";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
