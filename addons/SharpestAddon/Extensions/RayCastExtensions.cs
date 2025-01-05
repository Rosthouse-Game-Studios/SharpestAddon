using Godot;

namespace rosthouse.sharpest.addon;

public static class RayCastExtensions
{

  public static bool TryIsCollding(this RayCast3D r, out Node3D collider)
  {
    collider = r.GetCollider() as Node3D;
    return collider != null;
  }
}

