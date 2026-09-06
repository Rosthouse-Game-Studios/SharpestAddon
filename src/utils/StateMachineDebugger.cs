using System;
using Godot;
using ImGuiNET;
using rosthouse.sharpest.addon.nodes.finitestatemachine;

namespace rosthouse.sharpest.addon.utils;


[GlobalClass]
public partial class StateMachineDebugger : Node2D
{
  [Export]
  public StateMachine StateMachine { get; private set; } = null!;
  [Export]
  private bool active = false;
  private Label label = null!;
  public override void _Ready()
  {
    base._Ready();
    label = new Label();
    AddChild(label);
    StateMachine.SwitchingState += OnStateSwitch;
  }

  private void OnStateSwitch(State previousState, State nextState)
  {
    GD.Print($"[{Owner.Name}] Switching from state {previousState.Name} to {nextState.Name}");
    label.Text = StateMachine.CurrentState?.Name;
  }

  public override void _Process(double delta)
  {
    if (active && !Engine.IsEditorHint())
    {
      ImGui.Begin($"StateMachine {StateMachine.Name}");

      var currentState = StateMachine.CurrentState;
      var stateName = currentState?.Name.ToString() ?? "Unknown";
      ImGui.BeginDisabled();
      ImGui.InputText("StateMachine", stateName.ToAsciiBuffer(), (uint)stateName.Length);
      ImGui.EndDisabled();

      RenderDebug();
      ImGui.End();
    }
  }

  public void RenderDebug()
  {
    foreach (var state in StateMachine.States)
    {
      if (state is IDebugUi debugUi)
      {
        if (ImGui.CollapsingHeader(state.Name))
        {
          debugUi.RenderDebugUi();
        }
      }
    }
  }

}
