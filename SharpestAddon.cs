using Godot;

#if TOOLS

namespace rosthouse.sharpest.addon;

[Tool]
public partial class SharpestAddon : EditorPlugin
{

  public override void _EnterTree()
  {
    this.AddCustomType("Quit", "Node", GD.Load<Script>("res://addons/SharpestAddon/Nodes/Quit.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Nodes/quit.svg"));
    this.AddCustomType("Draw3D", "Node", GD.Load<Script>("res://addons/SharpestAddon/Autoloads/Draw3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Autoloads/draw3d.svg"));
    this.AddCustomType("WindowManager", "Node", GD.Load<Script>("res://addons/SharpestAddon/Autoloads/WindowManager.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Autoloads/windowmanager.svg"));
    this.AddCustomType("ExtendedTransform3D", "Node3D", GD.Load<Script>("res://addons/SharpestAddon/Nodes/ExtendedRemoteTransform3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Nodes/ExtendedRemoteTransform3D.svg"));
    this.AddCustomType("DebugOverlay", "CanvasLayer", GD.Load<Script>("res://addons/SharpestAddon/Autoloads/DebugOverlay/DebugOverlay.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Autoloads/DebugOverlay/debug_overlay_icon.png"));
    this.AddCustomType("CSGStairs3D", "CSGBox3D", GD.Load<Script>("res://addons/SharpestAddon/Nodes/CsgStairs3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Nodes/CsgStairs3D.svg"));

    this.AddAutoloadSingleton("Draw3D", "res://addons/SharpestAddon/Autoloads/Draw3D.cs");
    this.AddAutoloadSingleton("WindowManager", "res://addons/SharpestAddon/Autoloads/WindowManager.cs");
    this.AddAutoloadSingleton("DebugOverLay", "Autoloads/DebugOverlay/debug_overlay.tscn");
    this.AddAutoloadSingleton("Draw2D", "Autoloads/DebugDraw/Draw2D.cs");
  }

  public override void _ExitTree()

  {
    this.RemoveAutoloadSingleton("Draw3D");
    this.RemoveAutoloadSingleton("WindowManager");
    this.RemoveAutoloadSingleton("DebugOverlay");
    this.RemoveAutoloadSingleton("Draw2D");

    this.RemoveCustomType("Quit");
    this.RemoveCustomType("Draw3D");
    this.RemoveCustomType("WindowManager");
    this.RemoveCustomType("ExtendedTransform3D");
    this.RemoveCustomType("DebugOverlay");
    this.RemoveCustomType("CSGStairs3D");
  }
}

#endif
