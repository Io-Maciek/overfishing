using Godot;
using Overfishing.Statics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class GameObserver : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	ushort number_of_players;
	ushort condition = 1;
	Node rodsCanvas;
	List<fishing_rod> rods = new List<fishing_rod>();
	List<trash_script> trashes = new List<trash_script>();

	PackedScene rod_scene;
	PackedScene trash_scene;
	CanvasLayer trash_layer;

	public bool GameEnded = false;

	[Signal]
	public delegate void TimeStopsEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rodsCanvas = GetNode("RodsLayer");
		trash_layer = (CanvasLayer)GetNode("CanvasLayer");
		rod_scene = ResourceLoader.Load<PackedScene>("res://Scenes/Prefabs/Rods/rod1.tscn");
		trash_scene = ResourceLoader.Load<PackedScene>("res://Scenes/Prefabs/Trash/Trash.tscn");

		number_of_players = (ushort)Overfishing.Statics.GameStaticInfo._PLAYERS.Count;
		Debug.WriteLine($"Nr of players: {number_of_players}");
		if (number_of_players == 1)
			condition = 0;

	}


	public void AddTrash()
	{
		var trash = (trash_script)trash_scene.Instance();
		trash.Start(this);
		trashes.Add(trash);
		if (!is_light)
			trash.ToggleAffectLightTrash();

		trash_layer.CallDeferred("add_child", trash);
	}


	public void AddRod(bool flipped_x, bool flipped_y)
	{
		var _rod_new = (fishing_rod)rod_scene.Instance();
		_rod_new.Randomize(flipped_x, flipped_y, rod_number, this);
		rods.Add(_rod_new);
		rodsCanvas.CallDeferred("add_child", _rod_new);

		if (!is_light)
		{
			Debug.WriteLine("TOGLUJE");
			_rod_new.ToggleAffectLightRod();
		}
	}

	bool is_light = true;

	public void ToggleLight()
	{
		foreach (var rod in rods)
			rod.ToggleAffectLightRod();


		foreach (var trash in trashes)
			trash.ToggleAffectLightTrash();

		is_light = !is_light;

	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }


	ushort dead = 0;
	private void _player_killed(PlayerScript player)
	{
		//TODO
		player.IsAlive = false;
		dead++;
		var diff = number_of_players - dead;

		Debug.WriteLine($"KILL: {player.fish.Name}\t\t left: {diff}");

		if (diff == condition)
		{
			var players = (GetNode("SceneLoader") as PlayersInit).Players;
			var winner = players[0];

			if (condition != 0)
			{
				winner = players.Where(x => x.IsAlive).First();
			}

			GameEnded = true;

			foreach (var rod in rods)
				rod.ToggleGameEnd();


			GetNode("CanvasLayer3").GetNode("Pause").QueueFree();
			(GetNode("CanvasLayer5").GetNode("EndGame") as EndGameScript).ShowMe(winner, condition != 0);
			Debug.WriteLine("KONIEC GRY, === CAMO FISH UNLOCKED");
			if (!Overfishing.Statics.GameStaticInfo._CONFIG_INSTANCE.CamoFishUnlocked)
			{
				Overfishing.Statics.GameStaticInfo._CONFIG_INSTANCE.CamoFishUnlocked = true;
				Overfishing.Statics.GameStaticInfo._CONFIG_INSTANCE.Save();
			}
			EmitSignal("TimeStopsEventHandler");
		}
	}


	bool first_rod_appeared = false;
	ushort rod_number = 0;
	ushort max_rod_number = 6;

	private void _TimeLabel_TimeUpdated(int minute, int second)
	{
		if (!GameEnded)
		{
			if (!first_rod_appeared && minute >= 0 && second >= 5)
			{
				Debug.WriteLine("Adding first rod");
				first_rod_appeared = true;
				rod_number++;
				AddRod(new Random().NextDouble() >= 0.5, false);
			}
			else if (first_rod_appeared && rod_number < max_rod_number && second % 30 == 0)
			{
				Debug.WriteLine("Adding rod");
				rod_number++;
				AddRod(new Random().NextDouble() >= 0.5, new Random().NextDouble() >= 0.5);
			}

			if (rod_number >= max_rod_number)
			{
				Debug.WriteLine("======AlphaFish UNLOCKED!!!"); // TODO
				if (!Overfishing.Statics.GameStaticInfo._CONFIG_INSTANCE.AlphaFishUnlocked)
				{
					Overfishing.Statics.GameStaticInfo._CONFIG_INSTANCE.AlphaFishUnlocked = true;
					Overfishing.Statics.GameStaticInfo._CONFIG_INSTANCE.Save();
				}
			}
		}

		AddTrash(); //TODO
	}

	internal void DeleteMe(trash_script trash_script)
	{
		trashes.Remove(trash_script);
		trash_script.QueueFree();
	}
}
