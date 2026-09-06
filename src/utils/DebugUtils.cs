using System;
using Godot;

namespace rosthouse.sharpest.addon.utils;

[Flags]
public enum LoggingLevel
{
  None = 0,
  Error = 1,
  Warning = 2,
  Info = 4,
  Debug = 8
}
public static class DebugUtils
{
  private static LoggingLevel Level =>
        ProjectSettings.GetSetting(Constants.Settings.TileSizeSetting, 16).As<LoggingLevel>();
  public static void PrintDebug(string s, LoggingLevel loggingLevel = LoggingLevel.Error)
  {

    if (Level.HasFlag(loggingLevel))
    {
      GD.Print(s);
    }
  }
}

