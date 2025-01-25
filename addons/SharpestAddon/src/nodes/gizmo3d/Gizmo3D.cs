#nullable enable

using Godot;
using System;
using System.Reflection;
using System.Reflection.Metadata;

namespace rosthouse.sharpest.addon;

public partial class Gizmo3D : Node3D
{
  public static uint MASK = 32;
  public static Gizmo3D Create(Node parent, Node? owner = null,
   Gizmo3D.TransformFlags translation = TransformFlags.X | TransformFlags.Y | TransformFlags.Z,
   Gizmo3D.TransformFlags rotation = TransformFlags.X | TransformFlags.Y | TransformFlags.Z)
  {
    var g = GD.Load<PackedScene>("res://addons/SharpestAddon/resources/scenes/gizmo_3d.tscn").Instantiate<Gizmo3D>();
    parent.AddChild(g);
    g.Owner = owner ?? parent.Owner;
    g.TranslationFlags = translation;
    g.RotationFlags = rotation;
    return g;
  }

  public enum ActionType
  {
    NONE,
    MOVE, ROTATE,
  }

  [Signal] public delegate void MovedEventHandler(Vector3 movment);
  [Signal] public delegate void RotatedEventHandler(Vector3 axis, float angle);
  [Export] public float Scaling { get; private set; } = 1f;
  [Export] public float TranslateSpeed { get; set; } = 0.01f;
  private Vector3 currentNormal;
  Vector2 dragStartPosition = new(0, 0);
  private Handle? currentHandle;


  private Node3D translate = null!;
  private Node3D rotate = null!;

  [Flags]
  public enum TransformFlags
  {
    X = 1 << 1,
    Y = 1 << 2,
    Z = 1 << 3,
  }
  private TransformFlags translationFlags;
  [Export]
  TransformFlags TranslationFlags
  {
    get => translationFlags; set
    {
      translationFlags = value;
      if (!Engine.IsEditorHint())
      {
        SetAxisActive(TranslationFlags, TransformFlags.X, "%XAxis");
        SetAxisActive(TranslationFlags, TransformFlags.Y, "%YAxis");
        SetAxisActive(TranslationFlags, TransformFlags.Z, "%ZAxis");
      }
    }
  }
  private TransformFlags rotationFlags;
  [Export]
  TransformFlags RotationFlags
  {
    get => rotationFlags; set
    {
      rotationFlags = value;
      if (!Engine.IsEditorHint())
      {
        SetAxisActive(RotationFlags, TransformFlags.X, "%XPlane");
        SetAxisActive(RotationFlags, TransformFlags.Y, "%YPlane");
        SetAxisActive(RotationFlags, TransformFlags.Z, "%ZPlane");
      }
    }
  }


  public override void _Ready()
  {
    base._Ready();

    translate = GetNode<Node3D>("Translate");
    rotate = GetNode<Node3D>("Rotate");

    VisibilityChanged += () => Rotation = Vector3.Zero;

    SetAxisActive(TranslationFlags, TransformFlags.X, "%XAxis");
    SetAxisActive(TranslationFlags, TransformFlags.Y, "%YAxis");
    SetAxisActive(TranslationFlags, TransformFlags.Z, "%ZAxis");


    SetAxisActive(RotationFlags, TransformFlags.X, "%XPlane");
    SetAxisActive(RotationFlags, TransformFlags.Y, "%YPlane");
    SetAxisActive(RotationFlags, TransformFlags.Z, "%ZPlane");
  }

  private void SetAxisActive(TransformFlags flags, TransformFlags check, string nodePath)
  {
    if ((flags & check) != check)
    {
      GetNode<Node3D>(nodePath).ProcessMode = ProcessModeEnum.Disabled;
      GetNode<MeshInstance3D>($"{nodePath}/Mesh").Visible = false;
    }
  }

  public override void _Input(InputEvent @event)
  {
    var st = (SceneTree)Engine.GetMainLoop();
    var vp = st.CurrentScene.GetViewport();

    if (@event is InputEventMouseButton ev)
    {

      if (Input.IsActionJustReleased("ui_left_click"))
      {
        GD.Print("released");
        if (currentHandle != null)
        {
          vp.SetInputAsHandled();
          currentHandle = null;
        }
      }

      if (Input.IsActionJustPressed("ui_left_click"))
      {
        var dss = PhysicsServer3D.SpaceGetDirectState(vp.World3D.Space);
        var res = dss.CastRayFromCamera(collisionMask: MASK, collideWithAreas: true);
        if (res.HasValue && res.Value.GetCollisionObject3D() is Handle h)
        {
          GD.Print($"Hit {res.Value.GetCollisionObject3D().Name}");
          vp.SetInputAsHandled();
          currentHandle = h;
          var mesh = res.Value.GetCollisionObject3D().GetNode<MeshInstance3D>("Mesh");
          switch (currentHandle.Mode)
          {
            case ActionType.MOVE:
              HandleTranslateClick(ev, mesh);
              break;
            case ActionType.ROTATE:
              HandleRotateClick(ev, mesh, res.Value.normal);
              break;
            case ActionType.NONE:
              break;
          }
        }
      }
    }

  }
  public override void _Process(double delta)
  {
    var size = (GlobalPosition - GetViewport().GetCamera3D().GlobalPosition).Length() * Scaling;
    Vector3 scale = new(size, size, size);
    translate.Scale = scale;
    rotate.Scale = scale;

    if (currentHandle == null)
    {
      return;
    }

    switch (currentHandle.Mode)
    {
      case ActionType.MOVE:
        HandleTranslation(currentHandle);
        GetViewport().SetInputAsHandled();
        break;
      case ActionType.ROTATE:
        HandleRotation(currentHandle, currentNormal);
        GetViewport().SetInputAsHandled();
        break;
      case ActionType.NONE:
        break;
    }
    dragStartPosition = GetViewport().GetMousePosition();
  }

  private void HandleTranslateClick(InputEventMouseButton @event, MeshInstance3D mesh)
  {
    if (Input.IsActionJustPressed("ui_left_click") && !@event.IsEcho())
    {
      GD.Print("clicked translate");
      dragStartPosition = @event.Position;
    }
  }

  private void HandleRotateClick(InputEventMouseButton @event, MeshInstance3D mesh, Vector3 normal)
  {
    if (@event.IsActionPressed("ui_left_click") && !@event.IsEcho())
    {
      dragStartPosition = @event.Position;
      currentNormal = normal;
    }
  }

  public void HandleTranslation(Handle h)
  {
    var cam = GetViewport().GetCamera3D();
    var mp = GetViewport().GetMousePosition();
    var step = (cam.UnprojectPosition(h.GlobalPosition) - cam.UnprojectPosition(GlobalPosition)).Normalized();

    var dis = mp - dragStartPosition;
    var output = step * dis;

    var dir = h.Position.Normalized();

    var diff = dir * (output.X + output.Y) / 15f;

    GD.Print($"Diff{diff}");

    Translate(diff);
    EmitSignal(nameof(Moved), diff);
  }

  public void HandleRotation(Handle h, Vector3 normal)
  {
    // material_override.albedo_color.a8 = 200
    var mp = GetViewport().GetMousePosition();
    var parentCenter = GetViewport().GetCamera3D().UnprojectPosition(GlobalPosition);
    var start = parentCenter.AngleToPoint(dragStartPosition);
    var angle = parentCenter.AngleToPoint(mp);
    var dir = (GetViewport().GetCamera3D().GlobalPosition - GlobalPosition).Normalized();

    var rotAngle = normal.Dot(dir) > 0 ? start - angle : angle - start;

    RotateObjectLocal(normal, rotAngle);
    EmitSignal(nameof(Rotated), normal, rotAngle);
  }

}
