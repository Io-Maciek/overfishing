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
		

        optionScreen = (OptionsScreenScript)GetParent().GetNode("OptionsScreen");

		if(GameStaticInfo._PLAYERS != null)
		{
			Visible = false;
            playerChooseScreen.ShowScreen(this, true);
		}
		else
		{
            Visible = true;
        }
	}

	private void _on_BtnStartGame_pressed()
	{
		playerChooseScreen.ShowScreen(this, false);
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



