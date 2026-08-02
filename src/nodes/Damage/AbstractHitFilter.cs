
using Godot;

namespace rosthouse.sharpest.addon.nodes;

[GlobalClass]
public abstract partial class AbstractHitFilter: Resource
{
   public abstract bool FilterHit(HitBox hitBox, HurtBox hurtBox);
}