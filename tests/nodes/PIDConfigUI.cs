using Godot;
using rosthouse.sharpest.addon.nodes;
using System;

public partial class PIDConfigUI : GridContainer
{
  [Export] private PIDConfig Config { get; set; } = null!;
  [Export] private PIDController Controller { get; set; } = null!;

  public override void _Ready()
  {
    base._Ready();
    var setPointSlider = GetNode<HSlider>("SetPointSlider");
    setPointSlider.ValueChanged += OnSetPointChanged;
    setPointSlider.SetValueNoSignal(Controller.Target);
    var proportionalSlider = GetNode<HSlider>("ProportionalSlider");
    proportionalSlider.ValueChanged += OnProportionalChanged;
    proportionalSlider.SetValueNoSignal(Config.Proportional);
    var integralSlider = GetNode<HSlider>("IntegralSlider");
    integralSlider.ValueChanged += OnIntegralChanged;
    integralSlider.SetValueNoSignal(Config.Integral);
    var derivativeSlider = GetNode<HSlider>("DerivativeSlider");
    derivativeSlider.ValueChanged += OnDerivativeChanged;
    derivativeSlider.SetValueNoSignal(Config.Derivative);
  }

  private void OnSetPointChanged(double value)
  {
    Controller.Target = (float)value;
  }

  private void OnProportionalChanged(double value)
  {
    Config.Proportional = (float)value;
  }

  private void OnIntegralChanged(double value)
  {
    Config.Integral = (float)value;
  }

  private void OnDerivativeChanged(double value)
  {
    Config.Derivative = (float)value;
  }
}
