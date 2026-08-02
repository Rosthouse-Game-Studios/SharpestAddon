using System;
using Godot;
using Godot.Collections;
using rosthouse.sharpest.addon.utils;

namespace rosthouse.sharpest.addon.nodes;

[GlobalClass]
public partial class HitBox : Area2D
{

    [Signal]
    public delegate void DamageDealtEventHandler(bool hit, Damage damage);

    [Export]
    public Array<AbstractHitFilter> HitFilters { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Monitorable = false;
        Monitoring = true;

        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        if(area is HurtBox hb)
        {
            DebugUtils.PrintDebug($"HitBox {Name} of {Owner.Name} hit {area.Name}");
            foreach(var filter in HitFilters)
            {
                if(filter.FilterHit(this, hb))
                {
                    DebugUtils.PrintDebug($"Hit on {hb.Name} was filtered by {filter}");
                    return;
                }        
            }

            var damage = new Damage
            {
                DamageDealer = Owner,
                DamageHitBox = this,
                Amount = 1
            };
            var damageDealt = hb.DealDamage(damage);

            EmitSignal(SignalName.DamageDealt, damageDealt, damage);
        }
    }

}