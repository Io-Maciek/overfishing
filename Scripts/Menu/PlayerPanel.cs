using Godot;
using Overfishing.Player;
using System;

public class PlayerPanel : Control
{
	Label PlayerName;
	Label Description;
	public Control Arrows;
	TextureRect fishTexture;

	public override void _Ready()
	{
		PlayerName = (Label)GetNode("PlayerName");
		PlayerName.Text = "";
		Arrows = (Control)GetNode("Arrows");
		Arrows.Visible = false;

		Description = (Label)GetNode("description");
		Description.Text = "";

		fishTexture = GetNode("FishTexture") as TextureRect;
		fishTexture.Texture = null;
	}

	public void SetPlayer(PlayerUIModel player = null)
	{
		if (player == null)
		{
			PlayerName.Text = string.Empty;
			fishTexture.Texture = null;
			Description.Text = "";
		}
		else
		{
			PlayerName.Text = player.AssignedDevice+"\n"+player.Fish.Name;
			fishTexture.Texture = ResourceLoader.Load(player.Fish.SpriteFullPath()) as Texture;
			Description.Text = player.Fish.ActionDescription;
		}
	}
}
