using Godot;

namespace rosthouse.sharpest.addon;

public static class GodotObjectExtensions
{

  public static bool TryGetMeta(this GodotObject r, StringName name, out Variant value)
  { 
    if(r.HasMeta(name)){
      value = r.GetMeta(name);
      return true;
    }
    value = default;
    return false;
  }
  public static bool TryGetMeta<[MustBeVariant] T>(this GodotObject r, StringName name, out T value)
  { 
    if(r.HasMeta(name)){
      value = r.GetMeta(name).As<T>();
      return true;
    }
    value = default;
    return false;
  }
}


