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
        public bool CamoFishUnlocked { get; set; }


        public static GameConfig CreateDefault()
        {
            return new GameConfig { IsFullScreen = true, AlphaFishUnlocked = false, CamoFishUnlocked = false };
        }

        internal static GameConfig Load()
        {
            if (GameStaticInfo._CONFIG_INSTANCE == null)
            {
                if (OS.GetName() == "HTML5")
                {
                    var _config = new Godot.File();
                    if (_config.FileExists(_APPDATA_FILE_PATH))
                    {
                        _config.Open(_APPDATA_FILE_PATH, Godot.File.ModeFlags.Read);
                        GameConfig configReadFromFile = IoFile.ReadFromString<GameConfig>(_config.GetAsText());
                        _config.Close();
                        GameStaticInfo._CONFIG_INSTANCE = configReadFromFile;
                    }
                    else
                        GameStaticInfo._CONFIG_INSTANCE = GameConfig.CreateDefault();
                }
                else
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
            }

            return GameStaticInfo._CONFIG_INSTANCE;
        }


        public void Save()
        {
            if (OS.GetName() == "HTML5")
            {
                var _config = new Godot.File();
                _config.Open(_APPDATA_FILE_PATH, Godot.File.ModeFlags.Write);
                _config.StoreString(IoFile.WriteToString(this));
                _config.Close();
            }
            else
            {
                GameDirectory.CheckExistanceOfAppdataDirectory();
                System.IO.File.WriteAllText(_APPDATA_FILE_PATH, IoFile.WriteToString(this));
            }
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
