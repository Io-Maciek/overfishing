using Godot;
using Overfishing.Statics;
using System;
using System.Diagnostics;

public class EndGameScript : Control
{
	//WorldEnvironment _env;
	Control MainPanel;

	public override void _Ready()
	{
		MainPanel = GetNode("MainPanel") as Control;
		MainPanel.Visible = true;
		//_env = (WorldEnvironment)GetNode("WorldEnvironment");
		//_env.Environment.DofBlurNearEnabled = false;
		Visible = false;
	}


	public void ShowMe(PlayerScript winner)
	{
        GetTree().Paused = true;

        Visible = true;
        Input.MouseMode = Input.MouseModeEnum.Visible;

        //_env.Environment.DofBlurNearEnabled = true;
        Debug.WriteLine("The winnder is " + winner.fish.Name);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    private void _on_BtnBackToMenu_pressed()
	{
		GameStaticInfo._PLAYERS = null;
		GetTree().Paused = false;
		Input.MouseMode = Input.MouseModeEnum.Visible;

		GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
	}

}


