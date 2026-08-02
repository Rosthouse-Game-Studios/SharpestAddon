
using Godot;

namespace rosthouse.sharpest.addon.nodes;

[GlobalClass]
public abstract partial class AbstractDamageFilter: Resource
{
   public abstract bool FilterDamage(Damage damage);
}