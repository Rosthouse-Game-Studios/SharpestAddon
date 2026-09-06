using System;
using System.Runtime.CompilerServices;
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

  public static Vector3 Set(this Vector3 v, Vector3.Axis axis, float value)
  {
    v[(int)axis] = value;
    return v;
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

  /// <summary>
  /// Smoothly moves this vector toward <paramref name="target"/> using frame-rate independent exponential decay.
  /// Applies the formula <c>Lerp(current, target, 1 - decayBase^rate)</c>. The default <paramref name="decayBase"/>
  /// of 0.5 gives a "half-life" property: after one unit of accumulated rate the distance to the target is halved.
  /// Values closer to 0 decay faster; values closer to 1 decay slower. Because the weight is derived from an
  /// exponential function of time, the result depends only on total elapsed time and not on how many frames it
  /// was divided into — unlike a plain Lerp with a constant weight.
  /// The caller is responsible for pre-multiplying <paramref name="rate"/> by delta time before passing it in.
  /// </summary>
  /// <param name="current">The current vector value.</param>
  /// <param name="target">The target vector to move toward.</param>
  /// <param name="rate">Decay rate, already multiplied by delta time. Higher values converge faster.</param>
  /// <param name="decayBase">Base of the exponential. Defaults to 0.5 (half-life semantics). Must be in (0, 1).</param>
  /// <returns>The damped vector, closer to <paramref name="target"/>.</returns>
  /// <seealso href="https://www.rorydriscoll.com/2016/03/07/frame-rate-independent-damping-using-lerp/"/>
  public static Vector2 Damp(this Vector2 current, Vector2 target, float rate, float decayBase = 0.5f)
  {
    return current.Lerp(target, 1f - Mathf.Pow(decayBase, rate));
  }

  /// <inheritdoc cref="Damp(Vector2, Vector2, float, float)"/>
  public static Vector3 Damp(this Vector3 current, Vector3 target, float rate, float decayBase = 0.5f)
  {
    return current.Lerp(target, 1f - Mathf.Pow(decayBase, rate));
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
      Y = v.Y,
    };
  }

  public static System.Numerics.Vector3 ToNumerics(this Vector3 v)
  {
    return new()
    {
      X = v.X,
      Y = v.Y,
      Z = v.Z,
    };
  }
  public static Vector2 ToGodot(this System.Numerics.Vector2 v)
  {
    return new()
    {
      X = v.X,
      Y = v.Y,
    };
  }

  public static Vector3 ToGodot(this System.Numerics.Vector3 v)
  {
    return new()
    {
      X = v.X,
      Y = v.Y,
      Z = v.Z,
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
