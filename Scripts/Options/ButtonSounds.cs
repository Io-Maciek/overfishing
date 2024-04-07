using Godot;
using System;
using System.Diagnostics;

public class ButtonSounds : Node
{

	public override void _Ready()
	{

	}

	private void _on_Button_pressed()
	{
		Debug.WriteLine("PRESSED BLU");
		// TODO
	}


	private void _on_Button_mouse_entered()
	{
		Debug.WriteLine("HOVER OVER BLU");
        // TODO
    }
}








