using Godot;
using System;
using Overfishing.Statics;
using System.Diagnostics;
using System.Linq;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class PlayersInit : Node
{
	Vector2[] playerPositions = new Vector2[4]
	{
		new Vector2(176,160),
		new Vector2(1072,152),
		new Vector2(144,526),
		new Vector2(1096,480),
	};

    public List<PlayerScript> Players = new List<PlayerScript>(4);

    public override void _Ready()
	{
		//var playermodel = ResourceLoader.Load<PackedScene>("res://Scenes/Prefabs/PlayerFish.tscn");
		for (int i = 0; i < GameStaticInfo._PLAYERS.Count; i++)
		{
			var playermodel = ResourceLoader.Load<PackedScene>(GameStaticInfo._PLAYERS[i].Fish.SceneFullPath());

			var main_node = (Node2D)playermodel.Instance();
			main_node.Name = "Player" + i;
			var _player_script = main_node.GetNode("Player") as PlayerScript;
			_player_script.SetPlayer(GameStaticInfo._PLAYERS[i], this);
			_player_script.Position = playerPositions[i];

			GetParent().GetNode("CanvasLayer").CallDeferred("add_child", main_node);
			_player_script.scene_root = GetTree().Root.GetNode("Scene") as Node2D;
			_player_script.Connect("KillPlayerEventHandler", GetTree().Root.GetNode("Scene"), "_player_killed");
			Players.Add(_player_script);
        }

		AbilitiesExceptions = new System.Collections.Generic.Dictionary<string, (Func<Vector2, Vector2>, List<string>)>();
	}

	public float MovementSpeed = 200.0f;

	public System.Collections.Generic.Dictionary<string, (Func<Vector2,Vector2>, List<string>)> AbilitiesExceptions;

	public Vector2 MovementServer(string device)
	{
		if (AbilitiesExceptions.Count == 0)
			return _applyNormalMovement(device);
		else
			return _applyAbilityMovement(device);
	}

	private Vector2 _applyAbilityMovement(string device)
	{
		Vector2 movement = _applyNormalMovement(device);

		foreach (var MovementException in AbilitiesExceptions)
		{
			if (MovementException.Value.Item2.Contains(device))
			{
				movement = MovementException.Value.Item1(movement);
			}
		}

		return movement;//.Normalized();
	}

	private Vector2 _applyNormalMovement(string device)
	{
        /*var x= Input.GetVector($"{device}_left", $"{device}_right", $"{device}_up", $"{device}_down")
			* MovementSpeed;*/ // does not work in WEB TODO

        var strength = new Vector2(
        Input.GetActionStrength($"{device}_right") - Input.GetActionStrength($"{device}_left"),
        Input.GetActionStrength($"{device}_down") - Input.GetActionStrength($"{device}_up")
    ).Normalized();


        return strength.Length() > .1f ? (strength* MovementSpeed) : Vector2.Zero; //return x;

	}

	private void _guppy_ability_over()
	{
		AbilitiesExceptions.Remove("Guppy");
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
