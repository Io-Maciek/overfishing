using Godot;
using System;
using System.Diagnostics;

public class GameObserver : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    ushort number_of_players;
    ushort condition = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        number_of_players = (ushort)Overfishing.Statics.GameStaticInfo._PLAYERS.Count;
        Debug.WriteLine($"Nr of players: {number_of_players}");
        if (number_of_players == 1)
            condition = 0;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }


    ushort dead=0;
    private void _player_killed(PlayerScript player)
    {
        //TODO
        player.IsAlive = false;
        dead++;
        var diff = number_of_players - dead;

        Debug.WriteLine($"KILL: {player.fish.Name}\t\t left: {diff}");

        if(diff == condition)
            Debug.WriteLine("KONIEC GRY");
    }
}
