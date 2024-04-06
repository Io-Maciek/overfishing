using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScriptPufferFish : PlayerScript
{
    //public bool AbilityIsOn = false;
    public Timer abilityTimer;

    public override void SetPlayer(PlayerUIModel player, PlayersInit movementServer)
    {
        base.SetPlayer(player, movementServer);
        abilityTimer = (Timer)GetNode("Timer");
        abilityTimer.Connect("timeout", this, "_pufferfish_ability_over");
    }


    private void _pufferfish_ability_over()
    {
        MovementServer.AbilitiesExceptions.Remove("PufferFish");
        (GetNode("Sprite") as Sprite).Texture = (fish as PufferFish).SpriteTexture;
    }
}
