using Godot;
using Overfishing.GameSaving;
using Overfishing.Statics;
using System;
using System.Diagnostics;

public class MainPanelScript : Control
{
	PlayerInputs playerChooseScreen;
	PanelAboutScript aboutScreen;
	OptionsScreenScript optionScreen;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;
		playerChooseScreen = (PlayerInputs)GetParent().GetNode("PlayerChooseScreen");
        aboutScreen = (PanelAboutScript)GetParent().GetNode("PanelAbout");
		playerChooseScreen.Visible = false;
		aboutScreen.Visible = false;
		Visible = true;

        optionScreen = (OptionsScreenScript)GetParent().GetNode("OptionsScreen");
	}

	private void _on_BtnStartGame_pressed()
	{
		playerChooseScreen.ShowScreen(this);
	}


	private void _on_BtnGoToOptions_pressed()
	{
		optionScreen.ShowOptions(this);
	}


	private void _on_BtnQuitGame_pressed()
	{
		GetTree().Quit();
	}

    private void _on_BtnAbout_pressed()
    {
		aboutScreen.ShowMe(this);
    }
}



