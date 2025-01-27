#nullable enable

using Godot;

namespace rosthouse.sharpest.addon;

public static class NodeExtensions
{

  /// <summary>
  /// Get's all children of a <see cref="Node"/> fitting a specific type. They have to be a <see cref="Variant"/>.
  ///
  /// This method does not guarantee any ordering of the nodes.
  /// </summary>
  /// <typeparam name="T">The type we whish the children to be. Must be a <see cref="Variant"/>.</typeparam>
  /// <param name="n">The node for which to search children</param>
  /// <param name="includeInternal">If nodes not visible in the scene tree should be considered.</param>
  /// <param name="recursive">If the scene tree should be searched recursively.</param>
  /// <returns>A <see cref="Godot.Collections.Array{T}"/>, with all children fitting <see cref="{T}"/>. Will always return at least an emtpy array.</returns>
  public static Godot.Collections.Array<T> GetChildren<[MustBeVariant] T>(this Node n, bool includeInternal = false, bool recursive = false) where T : Node
  {
    var arr = new Godot.Collections.Array<T>();
    foreach (var c in n.GetChildren(includeInternal))
    {
      if (c is T t)
      {
        arr.Add(t);
        if(recursive){
          arr.AddRange(t.GetChildren<T>(includeInternal, recursive));
        }
      }
    }
    return arr;
  }

  /// <summary>
  /// Checks if the current <see cref="Node"/> has any <see cref="{T}"/> as a child.
  /// </summary>
  /// <typeparam name="T">Must be a <see cref="Variant"/></typeparam>
  /// <param name="n">The node tha</param>
  /// <param name="includeInternal">If internal children should also be searched</param>
  /// <returns>true, if any fitting node was found, false otherwise.</returns>
  public static bool HasChild<[MustBeVariant] T>(this Node n, bool includeInternal = false) where T : Node
  {
    foreach (var c in n.GetChildren(includeInternal))
    {
      if (c is T)
      {
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Gets the first child of a <see cref="Node"/> fitting the type parameter.
  /// </summary>
  /// <typeparam name="T">A type of <see cref="Variant"/></typeparam>
  /// <param name="n">The node for which we want to get a child.</param>
  /// <param name="includeInternal">If internal nodes should also be considered.</param>
  /// <param name="recursive">If the tree should be searched recursively. This enables depth-first search</param>
  /// <returns>The found node, null otherwise</returns>
  public static T? GetFirstChildByType<[MustBeVariant] T>(this Node n, bool includeInternal = false, bool recursive = false) where T : Node
  {
    foreach (var c in n.GetChildren(includeInternal))
    {
      if (c is T)
      {
        return (T)c;
      }
      var t = c.GetFirstChildByType<T>(includeInternal);
      if (t != null)
      {
        return t;
      }
    }
    return null;
  }

  /// <summary>
  /// Returns any child of the <see cref="Node" /> that is of type T. This is not guaranteed to be the first child.
  /// </summary>
  /// <param name="n">The Node to request a child from.</param>
  /// <typeparam name="T">Any class inheriting from Node.</typeparam>
  /// <returns>An instance of <see cref="T" />, or null</returns>
  public static T? GetAnyChildOrNull<T>(this Node n) where T : Node
  {
    foreach (var c in n.GetChildren())
    {
      if (c is T t)
      {
        return t;
      }
    }
    return null;
  }

  /// <summary>
  /// Calls <see cref="Node.QueueFree"/> on all children of the calling node.
  /// </summary>
  /// <param name="n">Free all children of this node</param>
  /// <param name="includeInternal">If the internal children (not visible in the scene tree) should also be freed.</param>
  public static void QueueFreeChildren(this Node n, bool includeInternal = false)
  {
    foreach (var c in n.GetChildren(includeInternal))
    {
      c.QueueFree();
    }
  }
}
