using Godot;
using System;
using Overfishing.Statics;

public class OptionsScreenScript : Control
{
	public Control PreviousScreen;
	Button ToggleFullscreen;

	public override void _Ready()
	{
		Visible = false;
		PreviousScreen = this;
		ToggleFullscreen = GetNode("FullScreenToggle").GetNode("Label").GetNode("CheckButton") as Button;

        _setFromConfig();
    }

	private void _on_BtnBackToMenu_pressed()
	{
		if(PreviousScreen != null)
		{
			PreviousScreen.Visible = true;
			Visible = false;
		}
	}

	public void ShowOptions(Control previousScreen)
	{
		_setFromConfig();
        PreviousScreen = previousScreen;
		Visible = true;
		PreviousScreen.Visible = false;
	}

	void _setFromConfig()
	{
		ToggleFullscreen.Pressed = OS.WindowFullscreen;
		GameStaticInfo._CONFIG_INSTANCE.IsFullScreen = OS.WindowFullscreen;
		GameStaticInfo._CONFIG_INSTANCE.Save();
    }

    private void _on_fullscreen_toggled(bool button_pressed)
    {
		OS.WindowFullscreen = button_pressed;
        GameStaticInfo._CONFIG_INSTANCE.IsFullScreen = button_pressed;
        GameStaticInfo._CONFIG_INSTANCE.Save();
    }
}



