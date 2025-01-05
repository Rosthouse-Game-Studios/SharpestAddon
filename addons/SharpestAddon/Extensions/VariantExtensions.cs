#nullable enable
using Godot;

namespace rosthouse.sharpest.addon;

public static class VariantExtensions
{
  public static T? Get<[MustBeVariant] T>(this GodotObject godotObject, StringName propertyName)
  {
    return godotObject.Get(propertyName).As<T>();
  }

}

