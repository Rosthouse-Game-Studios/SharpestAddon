using System;
using Godot;
using Godot.Collections;

namespace rosthouse.sharpest.addon.autoloads.debug;


public partial class DebugOverlay : CanvasLayer
{
  private static DebugOverlay instance = null!;

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
  private Label label = null!;

  public DebugOverlay() : base(){
    if (instance != null)
    {
      QueueFree();
      return;
    }
    instance = this;
  }

  public override void _Ready()
  {
    label = GetNode<Label>("MarginContainer/Label");
    ProcessPriority = -1000;
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    var labelText = string.Empty;

    foreach (var (key, c) in values)
    {
      labelText += $"{key}: {c.Call()}\n";
    }

    label.Text = labelText;
  }

  public void SetStat(string statName, Func<Variant> a){
    SetStat(statName, Callable.From(a));
  }

  public void SetStat(string statname, Variant value){
    SetStat(statname, () => value);
  }

  public void SetStat(string statName, Callable c)
  {
    values[statName] = c;
  }

  public void RemoveStat(string statName)
  {
    values.Remove(statName);
  }
}
