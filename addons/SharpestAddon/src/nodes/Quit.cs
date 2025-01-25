using Godot;

namespace rosthouse.sharpest.addon.nodes1;

public partial class Quit : Node
{
  [Export] private string quitAction = "quit_game";
  [Export] private bool quitWhenMouseCaptured = false;
  public override void _Input(InputEvent @event)
  {
    if (OS.GetName() == "HTML5") return;
    if (quitWhenMouseCaptured && Input.MouseMode == Input.MouseModeEnum.Captured) return;
    // if (@event.IsActionPressed(quitAction)) GetTree().Quit();
  }
}
