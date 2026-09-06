using Godot;
using Godot.Collections;


namespace rosthouse.sharpest.addon.nodes.finitestatemachine;

[GlobalClass]
public partial class StateMachine : Node
{
  [Export]
  public State InitialState { get; private set; } = null!;
  public State CurrentState { get; private set; } = null!;
  public Array<State> States { get; private set; } = null!;

  [Signal]
  public delegate void SwitchingStateEventHandler(State previousState, State nextState);

  public override async void _Ready()
  {
    States = this.GetChildren<State>();
    foreach (var state in States)
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
    EmitSignal(SignalName.SwitchingState, CurrentState, nextState);
    GD.Print($"[{Owner.Name}] Switching from state {CurrentState.Name} to {nextState.Name}");
    CurrentState?.Exit();
    nextState.Enter(CurrentState);
    CurrentState = nextState;
  }

  public override void _UnhandledInput(InputEvent @event)
  {
    CurrentState?.HandleInput(@event);
  }

  public override void _Process(double delta)
  {
    CurrentState?.Update((float)delta);
  }

  public override void _PhysicsProcess(double delta)
  {
    CurrentState?.PhysicsUpdate((float)delta);
  }
}
