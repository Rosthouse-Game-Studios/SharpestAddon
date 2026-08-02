using System;
using Godot;

namespace rosthouse.sharpest.addon.utils;

public static class MathUtils
{
  public static bool Between<T>(T min, T max, T t) where T : IComparable
  {
    return t.CompareTo(min) >= 0 && t.CompareTo(max) < 0;
  }

  public static float ExpDecay(float x, float max, float c)
  {
    return max * (1 - Mathf.Pow(Mathf.E, -c * x));
  }
}
