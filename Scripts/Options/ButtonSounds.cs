using Godot;
using System;
using System.Diagnostics;

public class ButtonSounds : Node
{
	AudioStreamPlayer hover_audio;
	AudioStreamPlayer click_audio;

	public override void _Ready()
	{
		hover_audio = GetNode("hover") as AudioStreamPlayer;
		click_audio = GetNode("click") as AudioStreamPlayer;
	}



	private void _on_Button_pressed()
	{
		click_audio.Play();
	}


	private void _on_Button_mouse_entered()
	{
		hover_audio.Play();
	}
}








