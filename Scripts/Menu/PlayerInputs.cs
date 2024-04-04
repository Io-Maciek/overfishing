using Godot;
using Overfishing.Player;
using System;
using System.Collections.Generic;
using Overfishing.Statics;
using Overfishing.Scripts.Fishes;
using System.Diagnostics;
using System.Linq;

public class PlayerInputs : Control
{
    public List<PlayerUIModel> Players = new List<PlayerUIModel>(4);
    public PlayerPanel[] playerPanels = new PlayerPanel[4];

    Control MainScreen;
    Button btnStartGame;
    FishLoaderScript fishLoader;


    public override void _Ready()
    {
        MainScreen = (Control)GetParent().GetNode("MainPanel");
        btnStartGame = (Button)GetNode("BtnStartGame");
        btnStartGame.Disabled = true;
        btnStartGame.FocusMode = FocusModeEnum.None;

        fishLoader = (FishLoaderScript)GetNode("FishLoader");
        //Debug.WriteLine($"{String.Join(" ", fishLoader.AvailableFishes.Select(x=>x.SpriteFullPath()))}");

        for (int i = 0; i < 4; i++)
        {
            Players.Add(null);
            playerPanels[i] = (PlayerPanel)GetNode($"Player{i + 1}");
        }
        UpdatePlayers();
    }

    public override void _Process(float delta)
    {
        if (Visible == true)
        {
            SelectPlayers();
            ChoosePlayerFish();
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        if (Players.All(x => x == null))
            return;

        Players.Clear();
        fishLoader.GetAll();

        for (int i = 0; i < 4; i++)
        {
            Players.Add(null);
            playerPanels[i] = (PlayerPanel)GetNode($"Player{i + 1}");
            playerPanels[i].Arrows.Visible = false;
        }
        UpdatePlayers();
    }

    private void ChoosePlayerFish()
    {
        foreach (var player in Players)
        {
            if (player == null)
                continue;

            if (fishLoader.AvailableFishes.Count > 0)
            {
                if (Input.IsActionJustPressed($"{player.AssignedDevice.ToLower()}_right"))
                {
                    var b = fishLoader.AvailableFishes.Count;
                    fishLoader.AvailableFishes.Add(player.Fish);
                    player.Fish = fishLoader.AvailableFishes[0];
                    fishLoader.AvailableFishes.Remove(player.Fish);
                    _reloadOnUI();
                }
                else if (Input.IsActionJustPressed($"{player.AssignedDevice.ToLower()}_left"))
                {
                    var b = fishLoader.AvailableFishes.Count;
                    fishLoader.AvailableFishes.Insert(0, player.Fish);
                    player.Fish = fishLoader.AvailableFishes[fishLoader.AvailableFishes.Count - 1];
                    fishLoader.AvailableFishes.Remove(player.Fish);
                    _reloadOnUI();
                }
            }
        }
    }


    private void SelectPlayers()
    {
        if (Input.IsActionJustPressed("keyboard_select"))
        {
            UpdatePlayers("Keyboard");
        }

        if (Input.IsActionJustPressed("pad1_select"))
        {
            UpdatePlayers("pad1");
        }

        if (Input.IsActionJustPressed("pad2_select"))
        {
            UpdatePlayers("pad2");
        }

        if (Input.IsActionJustPressed("pad3_select"))
        {
            UpdatePlayers("pad3");
        }

        if (Input.IsActionJustPressed("pad4_select"))
        {
            UpdatePlayers("pad4");
        }
    }

    PlayerUIModel _getSelectedPlayer(string deviceName)
    {
        PlayerUIModel changedPlayer = null;

        for (int i = 0; i < 4; i++)
        {
            if (Players[i] != null && Players[i].AssignedDevice == deviceName)
            {
                changedPlayer = Players[i];
                break;
            }
        }

        return changedPlayer;
    }

    void UpdatePlayers(string deviceName = null)
    {
        if (deviceName != null)
        {
            PlayerUIModel changedPlayer = _getSelectedPlayer(deviceName);

            if (changedPlayer != null) // delete player
            {
                _deletePlayer(changedPlayer);
            }
            else if(Players.Where(x=>x != null).Count() < 4) // add player
            {
                _addPlayer(deviceName);
            }
        }

        _reloadOnUI();
    }

    private ushort _searchForFirstNull()
    {
        ushort firstNull = 0;
        for (ushort i = 0; i < 4; i++)
        {
            if (Players[i] == null)
            {
                firstNull = i;
                break;
            }
        }

        return firstNull;
    }

    private void _addPlayer(string deviceName)
    {
        var firstNull = _searchForFirstNull();
        Players.RemoveAt(firstNull);
        Players.Insert(firstNull, new PlayerUIModel() { AssignedDevice = deviceName, Fish = fishLoader.AvailableFishes[0] });

        fishLoader.AvailableFishes.RemoveAt(0);
        //Debug.WriteLine($"Delete:\t\t{String.Join(" ; ", fishLoader.AvailableFishes.Select(x => x.Name))}");
    }

    private void _deletePlayer(PlayerUIModel changedPlayer)
    {
        fishLoader.AvailableFishes.Insert(0, changedPlayer.Fish);
        int index = Players.IndexOf(changedPlayer);
        Players[index] = null;
        playerPanels[index].Arrows.Visible = false;

        //Debug.WriteLine($"Delete:\t\t{String.Join(" ; ", fishLoader.AvailableFishes.Select(x => x.Name))}");
    }

    void _reloadOnUI()
    {
        ushort _players_number = 0;
        string output = "";
        for (int i = 0; i < 4; i++)
        {
            if (Players[i] == null)
            {
                output += "None";
                playerPanels[i].SetPlayer();

            }
            else
            {
                playerPanels[i].SetPlayer(Players[i]);
                output += Players[i].AssignedDevice;
                _players_number++;
            }

            output += " : ";
        }

        if (_players_number == 0)
        {
            btnStartGame.Disabled = true;
            btnStartGame.FocusMode = FocusModeEnum.None;
        }
        else
        {
            btnStartGame.Disabled = false;
            btnStartGame.FocusMode = FocusModeEnum.All;
        }



        if (fishLoader.AvailableFishes.Count > 0)
        {
            for (int i = 0; i < playerPanels.Length; i++)
            {
                if (Players[i] == null)
                    playerPanels[i].Arrows.Visible = false;
                else
                    playerPanels[i].Arrows.Visible = true;
            }
        }
        else
        {
            for (int i = 0; i < playerPanels.Length; i++)
            {
                playerPanels[i].Arrows.Visible = false;
            }
        }
    }

    private void _on_BtnBackToMenu_pressed()
    {
        MainScreen.Visible = true;
        Visible = false;
    }

    private void _on_BtnStartGame_pressed()
    {
        GameStaticInfo._PLAYERS = Players;
        GameStaticInfo._PLAYERS.RemoveAll(x => x == null);

        GetTree().ChangeScene("res://Scenes/GameScene.tscn");
    }
}
