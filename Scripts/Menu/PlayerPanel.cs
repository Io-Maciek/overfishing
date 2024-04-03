using Godot;
using Overfishing.Player;
using System;

public class PlayerPanel : Control
{
    Label PlayerName;

    public override void _Ready()
    {
        PlayerName = (Label)GetNode("PlayerName");
    }

    public void SetPlayer(PlayerUIModel player = null)
    {
        if (player == null)
        {
            PlayerName.Text = string.Empty;
        }
        else
        {
            PlayerName.Text = player.AssignedDevice+"\n"+player.Fish.Name;
        }
    }
}
