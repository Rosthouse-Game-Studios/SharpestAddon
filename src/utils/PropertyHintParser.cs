using System;
using System.Diagnostics.CodeAnalysis;

namespace rosthouse.sharpest.addon.utils;

public static class PropertyHintParser
{

  ///<summary>
  /// Checks if the given property string contains the hint. This checks only if
  /// any instance of the given hint is found and ignores repeated mentiones of
  /// the hint
  /// </summary>
  public static bool HasPropertyHint(
      string hintString,
      string hint
  )
  {
    return hintString.Contains(hint);
  }
  /// <summary>
  /// Tries to extract the value of a named entry from an Export hint string,
  /// e.g. given [Export(PropertyHint.None, "suffix:m/s")] and the requested
  /// hint "suffix", this returns true and "m/s". Hint entries are comma
  /// separated, so this also works for range hints ("0,1,0.1,suffix:x").
  /// If several entries match, the first one wins.
  /// </summary>
  public static bool TryGetPropertyHint(
      string hintString,
      string hint,
      [NotNullWhen(true)] out string? value
  )
  {
    var prefix = $"{hint}:";
    foreach (var part in hintString.Split(','))
    {
      var token = part.Trim();
      if (token.StartsWith(prefix, StringComparison.Ordinal))
      {
        value = token[prefix.Length..].Trim();
        return true;
      }
    }

    value = null;
    return false;
  }
}
