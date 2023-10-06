using Godot;
using Godot.Collections;

namespace rosthouse.sharpest.addon
{

  public partial class DebugOverlay : CanvasLayer
  {
    private static DebugOverlay instance;

    public static DebugOverlay Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new DebugOverlay();
        }
        return instance;
      }
    }


    private Dictionary<string, Callable> values = new Dictionary<string, Callable>();
    private Label label;

    public override void _Ready()
    {
      if (instance != null)
      {
        this.QueueFree();
        return;
      }
      instance = this;
      label = GetNode<Label>("MarginContainer/Label");
      this.ProcessPriority = -1000;
    }


    public override void _Process(double delta)
    {
      base._Process(delta);
      var labelText = string.Empty;

      foreach (var (key, c) in this.values)
      {
        labelText += $"{key}: {c.Call()}\n";
      }

      this.label.Text = labelText;
    }


    public override void _Notification(int what)
    {
      base._Notification(what);
    }

    public void AddStat(string statName, Callable c)
    {
      this.values[statName] = c;
    }

    public void RemoveStat(string statName)
    {
      this.values.Remove(statName);
    }
  }

}
