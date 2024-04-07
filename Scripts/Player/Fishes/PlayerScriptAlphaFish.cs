using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScriptAlphaFish : PlayerScript
{
    //public bool AbilityIsOn = false;
    public Timer abilityTimer;

    public override void SetPlayer(PlayerUIModel player, PlayersInit movementServer)
    {
        base.SetPlayer(player, movementServer);
        abilityTimer = (Timer)GetNode("Timer");
        abilityTimer.Connect("timeout", this, "_ability_over");
    }

    private void _ability_over()
    {
        MovementServer.AbilitiesExceptions.Remove("Alpha fish");
    }
}
