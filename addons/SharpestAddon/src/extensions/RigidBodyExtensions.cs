using Godot;

namespace rosthouse.sharpest.addons;

public static class RigidBodyExtensions
{
  public static Vector3 GetPointVelocity(this RigidBody3D r, Vector3 point)
  {
    return r.LinearVelocity + r.AngularVelocity.Cross(point - r.GlobalTransform.Origin);
  }
}
