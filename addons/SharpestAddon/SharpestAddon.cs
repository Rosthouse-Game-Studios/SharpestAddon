using Godot;
using rosthouse.sharpest.addon.autoloads;
using rosthouse.sharpest.addon.autoloads.debug;
using rosthouse.sharpest.addon.nodes;
using rosthouse.sharpest.addon.nodes1;

#if TOOLS

namespace rosthouse.sharpest.addon;

[Tool]
public partial class SharpestAddon : EditorPlugin {

  public override void _EnterTree() {
    AddCustomType(nameof(Quit), "Node", GD.Load<Script>("res://addons/SharpestAddon/src/nodes/Quit.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/assets/icons/quit.svg"));
    AddCustomType(nameof(Draw3D), "Node", GD.Load<Script>("res://addons/SharpestAddon/src/autoloads/Draw3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/assets/icons/draw3d.svg"));
    AddCustomType(nameof(WindowManager), "Node", GD.Load<Script>("res://addons/SharpestAddon/src/autoloads/WindowManager.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/assets/icons/windowmanager.svg"));
    AddCustomType(nameof(ExtendedRemoteTransform3D), "Node3D", GD.Load<Script>("res://addons/SharpestAddon/src/nodes/ExtendedRemoteTransform3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/assets/icons/ExtendedRemoteTransform3D.svg"));
    AddCustomType(nameof(DebugOverlay), "CanvasLayer", GD.Load<Script>("res://addons/SharpestAddon/src/autoloads/DebugOverlay.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/assets/icons/debug_overlay_icon.png"));
    AddCustomType(nameof(CsgStairs3D), "CSGBox3D", GD.Load<Script>("res://addons/SharpestAddon/src/nodes/CsgStairs3D.cs"), GD.Load<Texture2D>("res://addons/SharpestAddon/assets/icons/CsgStairs3D.svg"));

    AddAutoloadSingleton(nameof(Draw3D), "res://addons/SharpestAddon/src/autoloads/Draw3D.cs");
    AddAutoloadSingleton(nameof(WindowManager), "res://addons/SharpestAddon/src/autoloads/WindowManager.cs");
    AddAutoloadSingleton(nameof(DebugOverlay), "res://addons/SharpestAddon/resources/scenes/debug_overlay.tscn");
    AddAutoloadSingleton(nameof(Draw2D), "res://addons/SharpestAddon/src/autoloads/Draw2D.cs");
    AddAutoloadSingleton(nameof(LaunchManager), "res://addons/SharpestAddon/src/autoloads/LaunchManager.cs");
  }

  public override void _ExitTree() {
    RemoveAutoloadSingleton(nameof(Draw3D));
    RemoveAutoloadSingleton(nameof(WindowManager));
    RemoveAutoloadSingleton(nameof(DebugOverlay));
    RemoveAutoloadSingleton(nameof(Draw2D));

    RemoveCustomType(nameof(Quit));
    RemoveCustomType(nameof(Draw3D));
    RemoveCustomType(nameof(WindowManager));
    RemoveCustomType(nameof(ExtendedRemoteTransform3D));
    RemoveCustomType(nameof(DebugOverlay));
    RemoveCustomType(nameof(CsgStairs3D));
  }
}

#endif
