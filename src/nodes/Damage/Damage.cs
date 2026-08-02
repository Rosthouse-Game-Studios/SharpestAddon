using Godot;

namespace rosthouse.sharpest.addon.nodes;

public partial class Damage : GodotObject
{
    public Node DamageDealer { get; init; }
    public HitBox DamageHitBox { get; init; }
    public int Amount { get; init; }
}
