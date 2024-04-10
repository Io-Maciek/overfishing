using System;
using System.Diagnostics;
using System.IO;
using Godot;
using Overfishing.Statics;

namespace Overfishing.GameSaving
{
	public static class GameDirectory
	{
		public static string _APPDATA_PATH;

		static GameDirectory()
		{
			if(OS.GetName() == "HTML5")
			{
                _APPDATA_PATH = @"user://";
				GameConfig._APPDATA_FILE_PATH = $"{_APPDATA_PATH}{GameConfig._FILENAME}";

            }
            else
			{
                var appdata_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                _APPDATA_PATH = System.IO.Path.Combine(appdata_path, GameStaticInfo._GAME_DIRECTORY_NAME);
                GameConfig._APPDATA_FILE_PATH = System.IO.Path.Combine(_APPDATA_PATH, GameConfig._FILENAME);
            }
		}

		public static GameConfig LoadConfig()
		{
			return GameConfig.Load();
		}

		public static bool CheckExistanceOfAppdataDirectory(bool shouldCreateIfNone = true)
		{
			if (OS.GetName() == "HTML5")
				return true;

            bool do_exist = System.IO.Directory.Exists(_APPDATA_PATH);

			if (shouldCreateIfNone && !do_exist)
			{
                System.IO.Directory.CreateDirectory(_APPDATA_PATH);
				return true;
			}

			return do_exist;
		}
	}
}
