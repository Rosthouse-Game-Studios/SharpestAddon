
using Godot;

namespace rosthouse.sharpest.addon;

public static class Camera3DExtensions
{
  public static float GetZDepth(this Camera3D camera, Vector3 globalPosition)
  {
    var toPosition = globalPosition - camera.GlobalPosition;
    return toPosition.Dot(-camera.GlobalTransform.Basis.Z);
  }
}
