using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class Nemo : AFish
    {
        public override string SpriteName => "nemo";
        public override ulong AbilityCooldown => 60_000;

        public override float AbilityUseTime => 5.0f;
        public override string ActionDescription => $"Hides other players for {Mathf.RoundToInt(AbilityUseTime)} seconds.";


        string _name = "Nemo";
        public override string Name { get => _name; set => _name = value; }




        private ulong TimerOfNextAbility = 0;

        public override void Ability(Node2D GameSceneRoot)
        {
            if (Time.GetTicksMsec() < TimerOfNextAbility)
                return;
            base.Ability(GameSceneRoot);

            TimerOfNextAbility = Time.GetTicksMsec() + AbilityCooldown;

            Debug.WriteLine("===NEMO ABILITY:");

            var you = (PlayerScriptNemo)this.GetYourself(GameSceneRoot);
            foreach (var fish in GetOthers(GameSceneRoot))
            {
                fish.Visible = false;
            }



            you.abilityTimer.WaitTime = AbilityUseTime;
            you.abilityTimer.OneShot = true;
            you.abilityTimer.Start();
        }
    }
}
