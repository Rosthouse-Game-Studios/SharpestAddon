using System;
using System.Linq;
using Godot;
using rosthouse.sdd.vehicle;

namespace rosthouse.sharpest.addon.gizmos;

[Tool]
public partial class RigidBody3DGizmo : EditorNode3DGizmoPlugin
{

  private enum RigidBody3DGizmoHandles
  {
    CenterOfMass = 0
  }
  private const string RigidBody3DGizmoHandleMaterialName = "RigidBody3DGizmoHandleMaterialName";
  private const string RigidBody3DGizmoMaterialName = "RigidBody3DGizmoMaterialName";

  public RigidBody3DGizmo()
  {

    CreateMaterial(name: RigidBody3DGizmoMaterialName, color: Colors.Magenta, onTop: true);
    CreateHandleMaterial(name: RigidBody3DGizmoHandleMaterialName);
  }
  public override string _GetGizmoName()
  {
    return "RigidBody3DGizmo";
  }
  public override bool _HasGizmo(Node3D forNode3D)
  {
    var assignable = forNode3D.GetType().IsAssignableFrom(typeof(RigidBody3D));
    GD.Print($"Checking {forNode3D.Name} is RigidBody3D: {assignable}");
    return assignable;
  }
  public override void _Redraw(EditorNode3DGizmo gizmo)
  {
    base._Redraw(gizmo);
    gizmo.Clear();

    var rb = gizmo.GetNode3D() as RigidBody3D;
    if (rb is null)
    {
      return;
    }
    var handleMaterial = GetMaterial(RigidBody3DGizmoHandleMaterialName, gizmo);
    if (rb.CenterOfMassMode == RigidBody3D.CenterOfMassModeEnum.Custom)
    {
      gizmo.AddHandles([rb.CenterOfMass], handleMaterial, Array.Empty<int>());
    }
  }

  public override string _GetHandleName(EditorNode3DGizmo gizmo, int handleId, bool secondary)
  {
    return (RigidBody3DGizmoHandles)handleId switch
    {
      RigidBody3DGizmoHandles.CenterOfMass => nameof(RigidBody3DGizmoHandles.CenterOfMass),
      _ => string.Empty,
    };
  }

  public override Variant _GetHandleValue(EditorNode3DGizmo gizmo, int handleId, bool secondary)
  {
    var rb = gizmo.GetNode3D() as RigidBody3D;

    if (rb is null)
    {
      return Vector3.Zero;
    }

    return (RigidBody3DGizmoHandles)handleId switch
    {
      RigidBody3DGizmoHandles.CenterOfMass => (Variant)rb.CenterOfMass,
      _ => (Variant)string.Empty,
    };
  }

  public override void _SetHandle(EditorNode3DGizmo gizmo, int handleId, bool secondary, Camera3D camera, Vector2 screenPos)
  {
    var rb = gizmo.GetNode3D() as RigidBody3D;

    if (rb is null)
    {
      return;
    }

    var worldPos = camera.ProjectPosition(screenPos, camera.GetZDepth(rb.GlobalPosition + rb.CenterOfMass));

    switch ((RigidBody3DGizmoHandles)handleId)
    {
      case RigidBody3DGizmoHandles.CenterOfMass:
        rb.CenterOfMass = rb.ToLocal(worldPos);
        break;
      default:
        break;
    }

  }
}
