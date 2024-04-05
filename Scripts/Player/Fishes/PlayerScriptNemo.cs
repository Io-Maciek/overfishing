using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScriptNemo : PlayerScript
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
	}

    private void _on_Timer_timeout()
    {
		foreach (var fish in fish.GetOthers(GetTree().Root.GetNode("Scene") as Node2D))
		{
			fish.Visible = true;
		} 
    }
}



