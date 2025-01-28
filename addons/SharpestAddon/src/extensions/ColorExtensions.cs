using Godot;

namespace rosthouse.sharpest.addon;

public static class ColorExtensions
{

  private static readonly float DEG_30 = 30.0f / 360.0f;
  private static readonly float DEG_60 = 60.0f / 360.0f;
  private static readonly float DEG_90 = 90.0f / 360.0f;
  private static readonly float DEG_120 = 120.0f / 360.0f;
  private static readonly float DEG_150 = 150.0f / 360.0f;
  private static readonly float DEG_180 = 180.0f / 360.0f;
  private static readonly float DEG_210 = 210.0f / 360.0f;
  private static readonly float DEG_240 = 240.0f / 360.0f;
  private static readonly float DEG_270 = 270.0f / 360.0f;
  private static readonly float DEG_360 = 1f;

  /// <summary>
  /// Converts a color to a corresponding grayscale value. Note that only the RGB values are used for the conversion, the Alpha value is ignored.
  /// /// </summary>
  /// <param name="c">The color to convert</param>
  /// <returns>The converted color.</returns>
  public static Color ToGrayscale(this Color c)
  {
    var gray = (c.R + c.G + c.B) / 3;
    return new Color(gray, gray, gray, c.A);
  }

  /// <summary>
  /// Calculates a complementary color to the given <see cref="Color"/>
  ///
  /// Base on the following calculation: $h_1 = |(h_0 + 180°) mod 360°|$
  /// </summary>
  /// <param name="c">An input color</param>
  /// <returns>The complementary color to the given color</returns>
  public static Color Complementary(this Color c)
  {
    c.ToHsv(out var hue, out var sat, out var val);
    return Color.FromHsv(AddAngle(hue, DEG_180), sat, val, c.A);
  }

  /// <summary>
  /// Calculates split complementary colors to the given <see cref="Color"/>
  ///
  /// Base on the following calculation:
  /// $h_1 = |(h_0 + 150°) mod 360°|$
  /// $h_2 = |(h_0 + 210°) mod 360°|$
  /// </summary>
  /// <param name="c">An input color</param>
  /// <returns>A touple of two split complementary colors</returns>
  public static (Color a, Color b) SplitComplementary(this Color c)
  {
    c.ToHsv(out var hue, out var sat, out var val);
    return (
        Color.FromHsv(AddAngle(hue, DEG_150), sat, val, c.A),
        Color.FromHsv(AddAngle(hue, DEG_210), sat, val, c.A));
  }

  /// <summary>
  /// Calculates triadic colors to the given <see cref="Color"/>
  ///
  /// Base on the following calculation:
  /// $h_1 = |(h_0 + 120°) mod 360°|$
  /// $h_2 = |(h_0 + 240°) mod 360°|$
  /// </summary>
  /// <param name="c">An input color</param>
  /// <returns>A touple of two split complementary colors</returns>
  public static (Color a, Color b) Triadic(this Color c)
  {
    c.ToHsv(out var hue, out var sat, out var val);

    return (
        Color.FromHsv(AddAngle(hue, DEG_120), sat, val, c.A),
        Color.FromHsv(AddAngle(hue, DEG_240), sat, val, c.A));
  }


  /// <summary>
  /// Calculates tetradic colors to the given <see cref="Color"/>
  ///
  /// Base on the following calculation:
  /// $h_1 = |(h_0 +  90°) mod 360°|$
  /// $h_2 = |(h_0 + 180°) mod 360°|$
  /// $h_3 = |(h_0 + 270°) mod 360°|$
  /// </summary>
  /// <param name="c">An input color</param>
  /// <returns>A touple of two split complementary colors</returns>
  public static (Color a, Color b, Color c) Tetradic(this Color c)
  {
    c.ToHsv(out var hue, out var sat, out var val);

    return (
        Color.FromHsv(AddAngle(hue, DEG_90), sat, val, c.A),
        Color.FromHsv(AddAngle(hue, DEG_180), sat, val, c.A),
        Color.FromHsv(AddAngle(hue, DEG_270), sat, val, c.A));
  }

  /// <summary>
  /// Calculates tetradic colors to the given <see cref="Color"/>
  ///
  /// Base on the following calculation:
  /// $h_1 = |(h_0 + 30°) mod 360°|$
  /// $h_2 = |(h_0 + 60°) mod 360°|$
  /// $h_3 = |(h_0 + 90°) mod 360°|$
  /// </summary>
  /// <param name="c">An input color</param>
  /// <returns>A touple of two split complementary colors</returns>
  public static (Color a, Color b, Color c) Analogous(this Color c)
  {
    c.ToHsv(out var hue, out var sat, out var val);

    return (
        Color.FromHsv(AddAngle(hue, DEG_30), sat, val, c.A),
        Color.FromHsv(AddAngle(hue, DEG_60), sat, val, c.A),
        Color.FromHsv(AddAngle(hue, DEG_90), sat, val, c.A));
  }

  private static float AddAngle(float v, float val)
  {
    return (v + val) % 1.0f;
  }
}


