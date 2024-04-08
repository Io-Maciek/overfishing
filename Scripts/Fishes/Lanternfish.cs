using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class Lanternfish : AFish
    {
        public override string SpriteName => "lanternfish";

        public override ulong AbilityCooldown => 60_000;//(ulong)Mathf.RoundToInt(AbilityUseTime);//60_000;

        public override float AbilityUseTime => 5.0f;
        public override string ActionDescription => $"Turns of the light for {Mathf.RoundToInt(AbilityUseTime)} seconds.";


        string _name = "Lanternfish";
        public override string Name { get => _name; set => _name = value; }

        private ulong TimerOfNextAbility = 0;


        public override void Ability(Node2D GameSceneRoot)
        {
            base.Ability(GameSceneRoot);
            if (Time.GetTicksMsec() < TimerOfNextAbility)
                return;
            TimerOfNextAbility = Time.GetTicksMsec() + AbilityCooldown;

            Debug.WriteLine("===LANTERNFISH ABILITY");
            var you = (PlayerScriptLantern)GetYourself(GameSceneRoot);
            foreach(var others in GetOthers(GameSceneRoot))
            {
                (((Sprite)others.GetNode("Sprite")).Material as CanvasItemMaterial).LightMode = CanvasItemMaterial.LightModeEnum.LightOnly;
            }

            you.StartAbility();

            you.abilityTimer.WaitTime = AbilityUseTime;
            you.abilityTimer.OneShot = true;
            you.abilityTimer.Start();
        }

        public override string SpriteFullPath()
        {
            return "res://Images/Fishies/lanternfish_full.png";
        }
    }
}
