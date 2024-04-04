using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScriptLantern : PlayerScript
{
    public override void SetPlayer(PlayerUIModel player)
    {
        base.SetPlayer(player);

		(playerSprite.GetNode("AnimationPlayer") as AnimationPlayer).Play("idle");
    }

}
