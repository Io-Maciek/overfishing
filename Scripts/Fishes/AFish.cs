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


        private PlayerScript You = null;
        private List<PlayerScript> Others = null;

        protected PlayerScript GetYourself(Node2D RootNode)
        {
            if(Others == null)
            {
                //Debug.WriteLine("TWORZY INNYCH");
                Others = new List<PlayerScript>();
            }
            else
            {
                //Debug.WriteLine("INNI Z PAMIECI");
            }

            if (You != null)
            {
                //Debug.WriteLine("ZWRACAM CIEBIE Z PAMIECI\t"+You);
                return You;
            }
            else
            {
                //Debug.WriteLine("TWORZE CIEBIE\t" + You);
            }
            PlayerScript u = null;

            for (int i = 0; i < GameStaticInfo._PLAYERS.Count; i++)
            {
                var root = RootNode.GetNode("Player"+i);
                if (root == null)
                    continue;

                var player_from_root = root.GetNode("Player") as PlayerScript;
                if (player_from_root == null)
                    continue;
                

                var you_are_this_fish = player_from_root.fish == this;

                //Debug.WriteLine(you_are_this_fish+"\t\t"+player_from_root);
                if (you_are_this_fish)
                {
                    You = player_from_root;
                    u = You;
                }
                else
                    Others.Add(player_from_root);
            }

            return u;
        }
        protected List<PlayerScript> GetOthers(Node2D RootNode)
        {
            GetYourself(RootNode);
            return Others;
        }


    }
}
