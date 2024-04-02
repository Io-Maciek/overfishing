using System;
using System.IO;
using Overfishing.Statics;

namespace Overfishing.GameSaving
{
	public static class GameDirectory
	{
		public static string _APPDATA_PATH;

		static GameDirectory()
		{
			var appdata_path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			_APPDATA_PATH = Path.Combine(appdata_path, GameStaticInfo._GAME_DIRECTORY_NAME);
			GameConfig._APPDATA_FILE_PATH = Path.Combine(_APPDATA_PATH, GameConfig._FILENAME);
		}

		public static GameConfig LoadConfig()
		{
			return GameConfig.Load();
		}

		public static bool CheckExistanceOfAppdataDirectory(bool shouldCreateIfNone = true)
		{
			bool do_exist = Directory.Exists(_APPDATA_PATH);

			if (shouldCreateIfNone && !do_exist)
			{
				Directory.CreateDirectory(_APPDATA_PATH);
				return true;
			}

			return do_exist;
		}
	}
}
