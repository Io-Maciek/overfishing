using Godot;
using System;
using Overfishing.Statics;
using System.Diagnostics;
using System.Linq;

public class PlayersInit : Node
{
	Vector2[] playerPositions = new Vector2[4]
	{
		new Vector2(176,160),
		new Vector2(1072,152),
		new Vector2(144,526),
		new Vector2(1096,480),
	};

	public override void _Ready()
	{
		var playermodel = ResourceLoader.Load<PackedScene>("res://Scenes/Prefabs/PlayerFish.tscn");


		for (int i = 0; i < GameStaticInfo._PLAYERS.Count; i++)
		{
			var _player_script = (PlayerScript)playermodel.Instance();
			_player_script.SetPlayer(GameStaticInfo._PLAYERS[i]);
			_player_script.Position = playerPositions[i];

			GetParent().CallDeferred("add_child", _player_script);
		}

	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
