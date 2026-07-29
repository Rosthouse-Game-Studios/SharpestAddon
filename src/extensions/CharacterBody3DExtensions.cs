using Godot;

namespace rosthouse.sharpest.addon;


public static class CharacterBody3DExtensions
{
  public static bool IsSurfaceTooSteep(this CharacterBody3D body, Vector3 normal)
  {
    return normal.AngleTo(Vector3.Up) > body.FloorMaxAngle;
  }

  public static bool TestBodyMotion(this CharacterBody3D body, Transform3D from, Vector3 motion, out PhysicsTestMotionResult3D result)
  {
    result = new PhysicsTestMotionResult3D();

    var testParams = new PhysicsTestMotionParameters3D{
      From = from,
      Motion = motion
    };

    return PhysicsServer3D.BodyTestMotion(body.GetRid(), testParams, result);
  }
}