using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScriptGuppy : PlayerScript
{
    //public bool AbilityIsOn = false;
    public Timer abilityTimer;

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    public override void SetPlayer(PlayerUIModel player, PlayersInit movementServer)
    {
        base.SetPlayer(player, movementServer);
        abilityTimer = (Timer)GetNode("Timer");
        abilityTimer.Connect("timeout", movementServer, "_guppy_ability_over");
    }

}
