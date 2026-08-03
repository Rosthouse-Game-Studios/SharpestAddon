using System.Transactions;
using Godot;
using Godot.Collections;

namespace rosthouse.sharpest.addon.nodes.finitestatemachine;

[GlobalClass]
public abstract partial class State : Node
{
    [Signal]
    public delegate void FinishedEventHandler(NodePath nextState);

    protected void Finish(State nextState)
    {
        EmitSignal(SignalName.Finished, nextState.GetPath());
    } 

    public virtual void Enter(NodePath? previousState = null) { }

    public virtual void Exit() { }

    public virtual void HandleInput(InputEvent @event) { }

    public virtual void Update(double delta) { }

    public virtual void PhysicsUpdate(double delta) { }
}
