using Godot;
using Godot.Collections;
using rosthouse.sharpest.addon.utils;

namespace rosthouse.sharpest.addon.nodes;

[GlobalClass]
[Icon("res://addons/SharpestAddon/assets/icons/HurtBox2D.svg")]
public partial class HurtBox : Area2D
{

    [Signal]
    public delegate void DamageReceivedEventHandler(Damage damage);

    [Export]
    public Array<AbstractDamageFilter> DamageFilters { get; private set; } = [];

    public bool DealDamage(Damage damage)
    {
        DebugUtils.PrintDebug($"HurtBox {Name} of {Owner.Name} received damage from {damage.DamageDealer.Name}");
        var _filteredDamage = damage;
        foreach(var filter in DamageFilters)
        {
            if (filter.FilterDamage(_filteredDamage))
            {
                DebugUtils.PrintDebug($"Damage for HurtBox {Name} of {Owner.Name} was filtered by {filter}");
                return false;
            }
        }

        EmitSignal(SignalName.DamageReceived, damage);
        return true;
    }
}