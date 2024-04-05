using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScriptLantern : PlayerScript
{
    public override void SetPlayer(PlayerUIModel player, PlayersInit movementServer)
    {
        base.SetPlayer(player, movementServer);

		(playerSprite.GetNode("AnimationPlayer") as AnimationPlayer).Play("idle");
    }

}
