

using Godot.Collections;

public static class GodotCollectionsExtensions
{
	public static Array<T> Permutation<T>(this Array<T> a, bool deep = false)
	{
		var t = a.Duplicate(deep);
		t.Shuffle();
		return t;
	}
}