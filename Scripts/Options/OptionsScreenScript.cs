using Godot;
using System;

public class OptionsScreenScript : Control
{
	public Control PreviousScreen;

	public override void _Ready()
	{
    }

	private void _on_BtnBackToMenu_pressed()
	{
		if(PreviousScreen != null)
		{
			PreviousScreen.Visible = true;
			Visible = false;
		}
	}
}
