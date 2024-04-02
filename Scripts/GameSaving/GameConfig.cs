using System;
using System.IO;
using Overfishing.Statics;
using IoDeSer;

namespace Overfishing.GameSaving
{
	public class GameConfig
	{
		public const string _FILENAME = "config.io";
		public static string _APPDATA_FILE_PATH;

		public bool Variable { get; set; }

		public static GameConfig CreateDefault()
		{
			return new GameConfig { Variable = true };
		}

		internal static GameConfig Load()
		{
			if (GameStaticInfo._CONFIG_INSTANCE == null)
			{
				GameDirectory.CheckExistanceOfAppdataDirectory();
				if (File.Exists(_APPDATA_FILE_PATH))
				{

					GameConfig configReadFromFile = IoFile.ReadFromString<GameConfig>(File.ReadAllText(_APPDATA_FILE_PATH));
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
			File.WriteAllText(_APPDATA_FILE_PATH, IoFile.WriteToString(this));
		}
	}
}
