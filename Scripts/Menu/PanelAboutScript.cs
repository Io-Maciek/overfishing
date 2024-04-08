using Godot;
using System;

public class PanelAboutScript : Control
{
	
	public override void _Ready()
	{
		
	}


	Control previousScreen;
	public void ShowMe(Control previousScreen)
	{
		this.previousScreen = previousScreen;
		this.previousScreen.Visible = false;
		Visible = true;
	}

	private void _on_BtnBackToMenu_pressed()
	{
		this.previousScreen.Visible = true;
		Visible = false;
	}

}


