

using Godot;
using rosthouse.sharpest.addon.nodes.finitestatemachine;

namespace rosthouse.sharpest.addon.utils;


[GlobalClass]
public partial class StateMachineDebugger : Node2D
{
    [Export]
    public StateMachine StateMachine { get; private set; } = null!;
    private Label label = null!;
    public override void _Ready()
    {
        base._Ready();
        label = new Label();
        AddChild(label);
    }


    public override void _Process(double delta)
    {
        base._Process(delta);        

        label.Text = StateMachine.CurrentState.Name;
    }

}