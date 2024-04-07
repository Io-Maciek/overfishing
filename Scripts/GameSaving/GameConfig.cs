using System;
using System.IO;
using Overfishing.Statics;
using IoDeSer;
using Godot;
using System.Diagnostics;

namespace Overfishing.GameSaving
{
	public class GameConfig
	{
		public const string _FILENAME = "config.io";
		public static string _APPDATA_FILE_PATH;

		public bool IsFullScreen { get; set; }
		public bool AlphaFishUnlocked { get; set; }
		

		public static GameConfig CreateDefault()
		{
			return new GameConfig { IsFullScreen = true, AlphaFishUnlocked = false };
		}

		internal static GameConfig Load()
		{
			if (GameStaticInfo._CONFIG_INSTANCE == null)
			{
				GameDirectory.CheckExistanceOfAppdataDirectory();
				if (System.IO.File.Exists(_APPDATA_FILE_PATH))
				{
                    GameConfig configReadFromFile = IoFile.ReadFromString<GameConfig>(System.IO.File.ReadAllText(_APPDATA_FILE_PATH));
					GameStaticInfo._CONFIG_INSTANCE = configReadFromFile;
                }
                else
				{
					GameStaticInfo._CONFIG_INSTANCE = GameConfig.CreateDefault();
					//GameStaticInfo._CONFIG_INSTANCE.Save();
				}
			}

			return GameStaticInfo._CONFIG_INSTANCE;
		}


		public void Save()
		{
			GameDirectory.CheckExistanceOfAppdataDirectory();
			System.IO.File.WriteAllText(_APPDATA_FILE_PATH, IoFile.WriteToString(this));
		}

        internal void Apply()
        {
            OS.WindowFullscreen = IsFullScreen;
        }

        public override string ToString()
        {
			return IsFullScreen.ToString();
        }
    }
}
