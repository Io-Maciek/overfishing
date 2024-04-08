using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class GameObserver : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	ushort number_of_players;
	ushort condition = 1;
	Node rodsCanvas;
	List<fishing_rod> rods = new List<fishing_rod>();

	PackedScene rod_scene;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rodsCanvas = GetNode("RodsLayer");
		rod_scene = ResourceLoader.Load<PackedScene>("res://Scenes/Prefabs/Rods/rod1.tscn");

		number_of_players = (ushort)Overfishing.Statics.GameStaticInfo._PLAYERS.Count;
		Debug.WriteLine($"Nr of players: {number_of_players}");
		if (number_of_players == 1)
			condition = 0;

	}


	public void AddRod(bool flipped_x, bool flipped_y)
	{
		var _rod_new = (fishing_rod)rod_scene.Instance();
		_rod_new.Randomize(flipped_x, flipped_y, rod_number);
		rods.Add(_rod_new);
		rodsCanvas.CallDeferred("add_child", _rod_new);
	}

	public void ToggleRodLight()
	{
		foreach (var rod in rods)
			rod.ToggleAffectLight();
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
			Debug.WriteLine("KONIEC GRY");
	}


	bool first_rod_appeared = false;
	ushort rod_number = 0;
	ushort max_rod_number = 6;

	private void _TimeLabel_TimeUpdated(int minute, int second)
	{
		if(!first_rod_appeared && minute >= 0 && second >= 5)
		{
			Debug.WriteLine("Adding first rod");
			first_rod_appeared = true;
			rod_number++;
            AddRod(new Random().NextDouble() >= 0.5, false);
        }
		else if(first_rod_appeared && rod_number < max_rod_number && second %30 == 0)
		{
            Debug.WriteLine("Adding rod");
            rod_number++;
            AddRod(new Random().NextDouble() >= 0.5, new Random().NextDouble() >= 0.5);
		}
	}
}
