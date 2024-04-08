using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overfishing.Statics;

namespace Overfishing.Scripts.Fishes
{
    public abstract class AFish
    {
        public abstract string Name { get; set; }
        public abstract string SpriteName { get;  }
        public abstract string ActionDescription { get;  }
        public abstract ulong AbilityCooldown { get; }
        public abstract float AbilityUseTime { get; }

        public virtual void Ability(Node2D GameSceneRoot)
        {
            GetYourself(GameSceneRoot).audio.Play();
        }

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

        public PlayerScript GetYourself(Node2D RootNode)
        {
            var players = (RootNode.GetNode("SceneLoader") as PlayersInit).Players.Where(x=>x.IsAlive).ToList();

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].fish == this)
                {
                    return players[i];
                }
            }

            return null;
        }

        public List<PlayerScript> GetOthers(Node2D RootNode)
        {
            var Others = new List<PlayerScript>();

            var players = (RootNode.GetNode("SceneLoader") as PlayersInit).Players.Where(x => x.IsAlive).ToList();

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].fish != this)
                {
                    Others.Add(players[i]);
                }
            }

            return Others;
        }



    }
}
