using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScriptCamo : PlayerScript
{
	//public bool AbilityIsOn = false;
	public Timer abilityTimer;
	public bool CanBeHooked = true;

	public override void SetPlayer(PlayerUIModel player, PlayersInit movementServer)
	{
		base.SetPlayer(player, movementServer);
		abilityTimer = (Timer)GetNode("Timer");
        abilityTimer.Connect("timeout", this, "_on_Timer_timeout");
    }

    public void UseAbility()
	{
		CanBeHooked = false;
    }

    private void _on_Timer_timeout()
    {
        CanBeHooked = true;
    }
}



