using Godot;

namespace rosthouse.sharpest.addon.nodes;

public partial class Damage : GodotObject
{
    public required Node DamageDealer { get; init; }
    public required HitBox DamageHitBox { get; init; }
    public int Amount { get; init; }
}
