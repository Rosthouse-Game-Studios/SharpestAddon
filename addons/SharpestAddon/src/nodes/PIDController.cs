
using Godot;

namespace rosthouse.sharpest.addon.nodes;

[Tool]
[GlobalClass]
public partial class PIDController : Node
{

  [Export] public PIDConfig Config { get; set; } = null!;
  [Export] public float Target { get; set; }

  private float error = 0;
  private float integral = 0;
  private float proportional = 0;
  private float derivative = 0;

  public float Update(float measuredValue, float delta)
  {
    var currentError = Target - measuredValue;
    proportional = currentError;
    integral = integral + currentError * delta;
    derivative = (currentError - error) / delta;
    var output = Config.Proportional * proportional + Config.Integral * integral + Config.Derivative * derivative;
    error = currentError;
    return output;
  }

}

