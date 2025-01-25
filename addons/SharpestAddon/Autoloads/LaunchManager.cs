using Godot;
using rosthouse.rogueshooter;
using System.Linq;

namespace rosthouse.sharpest.addon.autoloads;

public partial class LaunchManager : Node
{

  public override void _Ready()
  {
    var dict = OS.GetCmdlineArgs().ToDictionary(v => v.TrimPrefix("--").Split("=").First(), v =>
    {
      var split = v.TrimPrefix("--").Split();
      return split.Count() == 2 ? split[1] : null;
    });

    if (dict.ContainsKey("server"))
    {
      var port = dict.ContainsKey("port") ? int.Parse(dict["port"]) : 1027;
      EventBus.Instance.RequestServer(port);
    }

    if(dict.TryGetValue("title", out var title)){
      GetViewport().GetWindow().Title = title;
    }
  }
}