using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class PufferFish : AFish
    {
        public override string SpriteName => "puffer";
        public override ulong AbilityCooldown => 60_000;
        public override float AbilityUseTime => 5.0f;
        public override string ActionDescription => $"Stops other players for {Mathf.RoundToInt(AbilityUseTime)} seconds.";


        string _name = "PufferFish";
        public override string Name { get => _name; set => _name = value; }



        private ulong TimerOfNextAbility = 0;

        public virtual string Sprite2FullPath()
        {
            return "res://Images/Fishies/puffer2.png";
        }

        Texture SpriteAbilityTexture;
        public Texture SpriteTexture;

        public PufferFish()
        {
            SpriteAbilityTexture = ResourceLoader.Load(Sprite2FullPath()) as Texture;
            SpriteTexture = ResourceLoader.Load(SpriteFullPath()) as Texture;
        }

        public override void Ability(Node2D GameSceneRoot)
        {
            base.Ability(GameSceneRoot);
            if (Time.GetTicksMsec() < TimerOfNextAbility)
                return;
            TimerOfNextAbility = Time.GetTicksMsec() + AbilityCooldown;

            Debug.WriteLine("===PUFFER ABILITY:");

            var you = (PlayerScriptPufferFish)this.GetYourself(GameSceneRoot);
            var others = this.GetOthers(GameSceneRoot);

            (you.GetNode("Sprite") as Sprite).Texture = SpriteAbilityTexture;

            you.MovementServer.AbilitiesExceptions.Add(
                Name,
                ((input)=>Vector2.Zero, others.Select(x => x.device).ToList())
            );

            you.abilityTimer.WaitTime = AbilityUseTime;
            you.abilityTimer.OneShot = true;
            you.abilityTimer.Start();
        }
    }
}
