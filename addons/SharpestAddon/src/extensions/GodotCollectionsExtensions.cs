using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace rosthouse.sharpest.addon;

public static class GodotCollectionsExtensions
{
  public static Array<T> Permutation<[MustBeVariant] T>(this Array<T> a, bool deep = false)
  {
    var t = a.Duplicate(deep);
    t.Shuffle();
    return t;
  }

  public static Array<T> ToGodotArray<[MustBeVariant] T>(this IEnumerable<T> t)
  {
    return new Array<T>(t.ToArray());
  }


  public static string PrettyPrint(this Dictionary d, bool singleLine = false)
  {
    return string.Join(singleLine ? ',' : '\n', d.Select(a => $"\t{a.Key}: {a.Value}"));
  }

  public static Godot.Collections.Dictionary<TKey, TElement> ToGodotDictionary<TSource, [MustBeVariant] TKey, [MustBeVariant] TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull
  {
    var godotDict = new Godot.Collections.Dictionary<TKey, TElement>();
    foreach (var src in source)
    {
      godotDict.Add(keySelector(src), elementSelector(src));
    }

    return godotDict;
  }
}
