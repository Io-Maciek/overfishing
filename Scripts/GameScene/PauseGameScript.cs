using Godot;
using System;
using Overfishing.Statics;
using System.Diagnostics;

public class PauseGameScript : Node
{
    string[] inputListeners;
    string pauseMaster = null;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;
        inputListeners = new string[GameStaticInfo._PLAYERS.Count];
        for (int i = 0; i < GameStaticInfo._PLAYERS.Count; i++)
        {
            inputListeners[i] = $"{GameStaticInfo._PLAYERS[i].AssignedDevice.ToLower()}_pause";
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (pauseMaster == null) // unpased
        {
            foreach (var actionKey in inputListeners)
            {
                if (Input.IsActionJustPressed(actionKey))
                {
                    PauseGame(actionKey);
                    break;
                }
            }
        }
        else // pasued
        {
            if (Input.IsActionJustPressed(pauseMaster))
            {
                UnPauseGame();
            }
        }


    }

    private void UnPauseGame()
    {
        Debug.WriteLine("UNPAUSED BY: " + pauseMaster);

        pauseMaster = null;
        Input.MouseMode = Input.MouseModeEnum.Hidden;
        GetTree().Paused = false;
    }

    private void PauseGame(string actionKey)
    {
        Debug.WriteLine("PAUSED BY: " + actionKey);
        pauseMaster = actionKey;
        Input.MouseMode = Input.MouseModeEnum.Confined;
        GetTree().Paused = true;
    }
}
