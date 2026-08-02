
using Godot;

namespace rosthouse.sharpest.addon.nodes;

[GlobalClass]
public partial class FilterSelfHit : AbstractHitFilter
{
    public override bool FilterHit(HitBox hitBox, HurtBox hurtBox)
    {
        return hitBox.Owner == hurtBox.Owner;
    }
}