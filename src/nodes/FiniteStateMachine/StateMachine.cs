using Godot;
using rosthouse.sharpest.addon.utils;


namespace rosthouse.sharpest.addon.nodes.finitestatemachine;

[GlobalClass]
public partial class StateMachine : Node
{
  [Export]
  public State InitialState { get; private set; } = null!;
  public State CurrentState { get; private set; } = null!;

  public override async void _Ready()
  {
    base._Ready();

    foreach (var state in this.GetChildren<State>())
    {
      state.Finished += TransitionNext;
    }

    await ToSignal(Owner, Node.SignalName.Ready);
    CurrentState = InitialState;
    CurrentState.Enter();
  }

  private void TransitionNext(NodePath nextStatePath)
  {
    var nextState = GetNode<State>(nextStatePath);
    DebugUtils.PrintDebug($"[{Owner.Name}] Switching from state {CurrentState.Name} to {nextState.Name}");
    CurrentState.Exit();
    nextState.Enter(CurrentState.GetPath());
    CurrentState = nextState;
  }

  public override void _UnhandledInput(InputEvent @event)
  {
    base._UnhandledInput(@event);
    CurrentState.HandleInput(@event);
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    CurrentState.Update(delta);
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
    CurrentState.PhysicsUpdate(delta);
  }
}
