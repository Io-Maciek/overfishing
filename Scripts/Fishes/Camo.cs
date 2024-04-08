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

        public override ulong AbilityCooldown => 60_000;

        public override float AbilityUseTime => 8.0f;


        public override string ActionDescription => $"Can't be hooked on fishing rod for {Mathf.RoundToInt(AbilityUseTime)} seconds.";


        string _name = "Camo";
        public override string Name { get => _name; set => _name = value; }

        private ulong TimerOfNextAbility = 0;

        public override void Ability(Node2D GameSceneRoot)
        {
            base.Ability(GameSceneRoot);
            if (Time.GetTicksMsec() < TimerOfNextAbility)
                return;

            TimerOfNextAbility = Time.GetTicksMsec() + AbilityCooldown;


            var you = (PlayerScriptCamo)GetYourself(GameSceneRoot);
            you.UseAbility();

            you.abilityTimer.WaitTime = AbilityUseTime;
            you.abilityTimer.OneShot = true;
            you.abilityTimer.Start();
        }
    }
}
