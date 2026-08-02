using System;
using Godot;

namespace rosthouse.sharpest.addon.utils;

public static class DebugUtils
{
    public static void PrintDebug(string s)
    {
        if (OS.IsStdOutVerbose())
        {
            GD.Print(s);
        }
    }
}

