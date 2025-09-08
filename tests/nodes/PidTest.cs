using Godot;
using rosthouse.sharpest.addon.nodes;

public partial class PidTest : Node3D
{
  private RigidBody3D ball = null!;
  private PIDController controller = null!;


  public override void _Ready()
  {
    base._Ready();
    ball = GetNode<RigidBody3D>("Ball");
    controller = GetNode<PIDController>("PIDController");
  }
  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);

    var force = controller.Update(ball.GlobalPosition.Y, (float)delta);
    ball.ApplyCentralForce(Vector3.Up * force);
  }
}
