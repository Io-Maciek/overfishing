using Godot;
using Overfishing.Player;
using System;
using System.Collections.Generic;
using Overfishing.Statics;

public class PlayerInputs : Control
{
	public List<PlayerUIModel> Players = new List<PlayerUIModel>(4);
	public PlayerPanel[] playerPanels = new PlayerPanel[4];

	Control MainScreen;
	Button btnStartGame;

	public override void _Ready()
	{
		MainScreen = (Control)GetParent().GetNode("MainPanel");
		btnStartGame = (Button)GetNode("BtnStartGame");
		btnStartGame.Disabled = true;
        btnStartGame.FocusMode = FocusModeEnum.None;

		//AddChild(ResourceLoader.Load<PackedScene>("").Instance()); // TODO as adding players to main scene

        for (int i = 0; i < 4; i++)
		{
			Players.Add(null);
			playerPanels[i] = (PlayerPanel)GetNode($"Player{i + 1}");
		}
		UpdatePlayers();
	}

	public override void _Process(float delta)
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
			else // add player
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
		Players.Insert(firstNull, new PlayerUIModel() { AssignedDevice = deviceName });
	}

	private void _deletePlayer(PlayerUIModel changedPlayer)
	{
		int index = Players.IndexOf(changedPlayer);
		Players[index] = null;
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