#nullable enable

using Godot;
using Godot.Collections;
using System.Linq;

namespace rosthouse.sharpest.addon.autoloads;

public partial class LaunchManager : Node
{
  [Signal] public delegate void CommandLineParsedEventHandler(Dictionary<string, string?> arguments);

  private static LaunchManager _instance = null!;

  public static LaunchManager Instance => _instance;

  public LaunchManager() : base()
  {
    if (_instance != null)
    {
      QueueFree();
    }
    else
    {
      _instance = this;
    }
  }
  public override void _Ready()
  {
    var dict = OS.GetCmdlineArgs().ToGodotDictionary(v => v.TrimPrefix("--").Split("=").First(), v =>
    {
      var split = v.TrimPrefix("--").Split('=');
      return split.Count() == 2 ? split[1] : null;
    });

    GD.Print(string.Join('\n', dict.Select(a => $"{a.Key}: {a.Value}")));

    EmitSignal(SignalName.CommandLineParsed, dict);
  }
}
