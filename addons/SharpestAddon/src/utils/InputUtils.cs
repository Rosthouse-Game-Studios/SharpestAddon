using Godot;

namespace rosthouse.sharpest.addon;

public static class InputUtils
{
  public static Vector3 GetHorizontalVector(StringName negativeX, StringName positiveX, StringName negativeY, StringName positiveY, float deadzone = -1f)
  {

    var inputDir = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");

    return new Vector3(
      inputDir.X,
      0,
      inputDir.Y
    );
  }

}