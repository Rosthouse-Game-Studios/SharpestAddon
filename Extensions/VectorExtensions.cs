using System;
using Godot;

namespace rosthouse.sharpest.addon;


public static class VectorExtensions
{

  public static int Xint(this Vector2 v)
  {
    return (int)v.X;
  }

  public static int Yint(this Vector2 v)
  {
    return (int)v.Y;
  }

  public static Vector2 GetFourWayDirection(int x)
  {
    switch (x)
    {
      case 0:
        return Vector2.Right;
      case 1:
        return Vector2.Up;
      case 2:
        return Vector2.Left;
      case 3:
        return Vector2.Down;
      default:
        throw new Exception($"Expected value between 0 and 3, got {x}");
    }
  }

  public static Vector2I RountToInt(this Vector2 v)
  {
    return new Vector2I(Mathf.RoundToInt(v.X), Mathf.RoundToInt(v.Y));
  }

  public static System.Numerics.Vector2 ToNumerics(this Vector2 v)
  {
    return new()
    {
      X = v.X,
      Y = v.Y
    };
  }

  public static Vector2 ToGodot(this System.Numerics.Vector2 v)
  {
    return new()
    {
      X = v.X,
      Y = v.Y
    };
  }

  public static Vector3I RoundToInt(this Vector3 v)
  {
    return new Vector3I(
        Mathf.RoundToInt(v.X),
        Mathf.RoundToInt(v.Y),
        Mathf.RoundToInt(v.Z)
    );
  }

  public static Vector4I RoundToInt(this Vector4 v)
  {
    return new Vector4I(
        Mathf.RoundToInt(v.X),
        Mathf.RoundToInt(v.Y),
        Mathf.RoundToInt(v.Z),
        Mathf.RoundToInt(v.W)
    );
  }


  /// <summary>
  /// Rounds the values of a <see cref="Vector3" /> to n decimal places.
  /// </summary>
  /// <param name="v">The vector to round</param>
  /// <param name="decimals">The number of decimals behind the decimal point to round the vector to.</param>
  /// <returns>A new rounded Vector3</returns>
  public static Vector3 Round(this Vector3 v, int decimals)
  {
    return new Vector3(
      (float)Math.Round(v.X, decimals),
      (float)Math.Round(v.Y, decimals),
      (float)Math.Round(v.X, decimals)
    );
  }
}
