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

        public abstract void Ability();

        public string SpriteFullPath()
        {
            string sprite_name = SpriteName.EndsWith(".png") ? SpriteName : $"{SpriteName}.png";
            return $"res://Images/Fishies/{sprite_name}";
        }
    }
}
