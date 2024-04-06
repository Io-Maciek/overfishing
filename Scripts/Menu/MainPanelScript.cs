using Godot;
using Overfishing.GameSaving;
using Overfishing.Statics;
using System;
using System.Diagnostics;

public class MainPanelScript : Control
{
    PlayerInputs playerChooseScreen;
	OptionsScreenScript optionScreen;

	public override void _Ready()
	{
        Input.MouseMode = Input.MouseModeEnum.Visible;
        playerChooseScreen = (PlayerInputs)GetParent().GetNode("PlayerChooseScreen");
		playerChooseScreen.Visible = false;

		optionScreen = (OptionsScreenScript)GetParent().GetNode("OptionsScreen");
	}

    private void _on_BtnStartGame_pressed()
	{
		playerChooseScreen.Visible = true;
		Visible = false;
	}


	private void _on_BtnGoToOptions_pressed()
	{
		optionScreen.ShowOptions(this);
	}


	private void _on_BtnQuitGame_pressed()
	{
		GetTree().Quit();
	}
}
