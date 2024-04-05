﻿using Godot;
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
        public override float AbilityUseTime => 10.0f;
        public override string ActionDescription => $"Stop other players for {Mathf.RoundToInt(AbilityUseTime)} seconds.";


        string _name = "PufferFish";
        public override string Name { get => _name; set => _name = value; }



        private ulong TimerOfNextAbility = 0;

        public override void Ability(Node2D GameSceneRoot)
        {
            if (Time.GetTicksMsec() < TimerOfNextAbility)
                return;
            TimerOfNextAbility = Time.GetTicksMsec() + AbilityCooldown;

            Debug.WriteLine("===PUFFER ABILITY:");

            var you = (PlayerScriptPufferfish)this.GetYourself(GameSceneRoot);
            var others = this.GetOthers(GameSceneRoot);

            you.MovementServer.AbilitiesExceptions.Add(
                Name,
                ((input)=>-1*input, others.Select(x => x.device).ToList())
            );

            you.abilityTimer.WaitTime = AbilityUseTime;
            you.abilityTimer.OneShot = true;
            you.abilityTimer.Start();
        }
    }
}
