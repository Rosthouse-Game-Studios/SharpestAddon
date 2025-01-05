using Godot;

namespace rosthouse.sharpest.addon;

public static class RayCastExtensions
{

  public static bool TryIsCollding(this RayCast3D r, out Node3D collider)
  {
#pragma warning disable CS8601 // Possible null reference assignment.
    collider = r.GetCollider() as Node3D;
#pragma warning restore CS8601 // Possible null reference assignment.
    return collider != null;
  }
}

