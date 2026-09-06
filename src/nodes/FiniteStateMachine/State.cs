using Godot;

namespace rosthouse.sharpest.addon.nodes.finitestatemachine;

[GlobalClass]
public abstract partial class State : Node
{
  [Signal]
  public delegate void FinishedEventHandler(NodePath nextState);

  protected void Finish(State nextState)
  {
    Callable.From(() => EmitSignal(SignalName.Finished, nextState.GetPath())).CallDeferred();
  }

  public virtual void Enter(State? previousState = null) { }

  public virtual void Exit() { }

  public virtual void HandleInput(InputEvent @event) { }

  public virtual void Update(float delta) { }

  public virtual void PhysicsUpdate(float delta) { }
}
