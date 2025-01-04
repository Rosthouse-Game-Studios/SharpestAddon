using Godot;


namespace rosthouse.sharpest.addon;

public partial class Quit : Node
{
    [Export] private string quitAction = "ui_end";
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(quitAction) && OS.GetName() != "HTML5")
        {
            GetTree().Quit();
        }
        base._Input(@event);
    }
}
