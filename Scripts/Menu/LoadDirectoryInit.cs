using Godot;
using Overfishing.GameSaving;
using System;
using System.Diagnostics;

public class LoadDirectoryInit : Node
{
	public override void _Ready()
	{
		GameDirectory.CheckExistanceOfAppdataDirectory();
		GameConfig _config = GameConfig.Load();

        ApplyConfigOptions(_config);
    }

    private void ApplyConfigOptions(GameConfig config)
    {
        config.Apply();
    }


}
