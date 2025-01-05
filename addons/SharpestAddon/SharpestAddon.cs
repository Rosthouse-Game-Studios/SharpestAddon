using Godot;

#if TOOLS

namespace rosthouse.sharpest.addon;

[Tool]
public partial class SharpestAddon : EditorPlugin {

  public override void _EnterTree() {
    AddCustomType("Quit", "Node", GD.Load<Script>("res://addons/SharpestAddon/Nodes/Quit.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Nodes/quit.svg"));
    AddCustomType("Draw3D", "Node", GD.Load<Script>("res://addons/SharpestAddon/Autoloads/Draw3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Autoloads/draw3d.svg"));
    AddCustomType("WindowManager", "Node", GD.Load<Script>("res://addons/SharpestAddon/Autoloads/WindowManager.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Autoloads/windowmanager.svg"));
    AddCustomType("ExtendedTransform3D", "Node3D", GD.Load<Script>("res://addons/SharpestAddon/Nodes/ExtendedRemoteTransform3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Nodes/ExtendedRemoteTransform3D.svg"));
    AddCustomType("DebugOverlay", "CanvasLayer", GD.Load<Script>("res://addons/SharpestAddon/Autoloads/DebugOverlay/DebugOverlay.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Autoloads/DebugOverlay/debug_overlay_icon.png"));
    AddCustomType("CSGStairs3D", "CSGBox3D", GD.Load<Script>("res://addons/SharpestAddon/Nodes/CsgStairs3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/Nodes/CsgStairs3D.svg"));

    AddAutoloadSingleton("Draw3D", "res://addons/SharpestAddon/Autoloads/Draw3D.cs");
    AddAutoloadSingleton("WindowManager", "res://addons/SharpestAddon/Autoloads/WindowManager.cs");
    AddAutoloadSingleton("DebugOverLay", "Autoloads/DebugOverlay/debug_overlay.tscn");
    AddAutoloadSingleton("Draw2D", "Autoloads/DebugDraw/Draw2D.cs");
  }

  public override void _ExitTree() {
    RemoveAutoloadSingleton("Draw3D");
    RemoveAutoloadSingleton("WindowManager");
    RemoveAutoloadSingleton("DebugOverlay");
    RemoveAutoloadSingleton("Draw2D");

    RemoveCustomType("Quit");
    RemoveCustomType("Draw3D");
    RemoveCustomType("WindowManager");
    RemoveCustomType("ExtendedTransform3D");
    RemoveCustomType("DebugOverlay");
    RemoveCustomType("CSGStairs3D");
  }
}

#endif
