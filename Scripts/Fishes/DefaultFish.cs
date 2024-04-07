using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class AlphaFish : AFish
    {
        public override string SpriteName => "alpha_fish";
        public override float AbilityUseTime => 10.0f;

        public override ulong AbilityCooldown => 60_000;
        public override string ActionDescription => $"Swims twice as fast for {Mathf.RoundToInt(AbilityUseTime)} seconds.";



        string _name = "Alpha fish";
        public override string Name { get => _name; set => _name = value; }



        private ulong TimerOfNextAbility = 0;

        public override void Ability(Node2D GameSceneRoot)
        {
            if (Time.GetTicksMsec() < TimerOfNextAbility)
                return;
            TimerOfNextAbility = Time.GetTicksMsec() + AbilityCooldown;

            Debug.WriteLine("===ALPHA FISH ABILITY:");

            var you = (PlayerScriptAlphaFish)this.GetYourself(GameSceneRoot);

            you.MovementServer.AbilitiesExceptions.Add(
                Name,
                ((input) => 2 * input, new List<string>{ you.device})
            );

            you.abilityTimer.WaitTime = AbilityUseTime;
            you.abilityTimer.OneShot = true;
            you.abilityTimer.Start();
        }
    }
}
