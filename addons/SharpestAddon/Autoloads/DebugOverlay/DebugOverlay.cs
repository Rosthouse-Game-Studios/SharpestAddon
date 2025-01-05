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

  public override void _Ready()
  {
    if (instance != null)
    {
      QueueFree();
      return;
    }
    instance = this;
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

  public void AddStat(string statName, Func<Variant> a){
    AddStat(statName, Callable.From(a));
  }

  public void AddStat(string statName, Callable c)
  {
    values[statName] = c;
  }

  public void RemoveStat(string statName)
  {
    values.Remove(statName);
  }
}
