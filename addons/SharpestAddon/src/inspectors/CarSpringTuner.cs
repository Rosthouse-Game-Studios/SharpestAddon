using Godot;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using rosthouse.sharpest.addon;
using System;

public partial class CarSpringTuner : EditorInspectorPlugin
{
  private const string PROPERTY_ZETA = "Zeta";
  private const string PROPERTY_FREQUENCY = "Frequency";

  public override bool _CanHandle(GodotObject @object)
  {
    return @object is VehicleBody3D;
  }

  public override void _ParseCategory(GodotObject @object, string category)
  {
    base._ParseCategory(@object, category);
    if (@object is VehicleBody3D vehicle && category == nameof(VehicleBody3D))
    {
      EditorSpinSlider frequency = new EditorSpinSlider
      {
        MinValue = 0,
        Step = 0,
        Label = "Frequency",
        Suffix = "Hz"
      };
      EditorSpinSlider zeta = new EditorSpinSlider
      {
        MinValue = 0,
        Step = 0,
        MaxValue = 2,
        Label = "Zeta"
      };
      if (@object.TryGetMeta<float>(PROPERTY_FREQUENCY, out var frequencyValue))
      {
        frequency.SetValueNoSignal(frequencyValue);
      }

      if (@object.TryGetMeta<float>(PROPERTY_ZETA, out var zetaValue))
      {
        zeta.SetValueNoSignal(zetaValue);
      }

      frequency.ValueChanged += v => OnFrequencyChanged(vehicle, v);
      zeta.ValueChanged += v => OnZetaChanged(vehicle, v);

      AddPropertyEditor(PROPERTY_FREQUENCY, frequency, label: "Frequency");
      AddPropertyEditor(PROPERTY_ZETA, zeta, label: "Zeta");
    }
  }

  private void OnZetaChanged(VehicleBody3D vehicle, double v)
  {
    vehicle.SetMeta(PROPERTY_ZETA, v);
    RecalculateSprings(vehicle);
  }

  private void OnFrequencyChanged(VehicleBody3D vehicleBody3D, double value)
  {
    vehicleBody3D.SetMeta(PROPERTY_FREQUENCY, value);
  }

  private void RecalculateSprings()
  {

  }

  private static float CalculateStiffness(float mass, float frequency)
  {
    return mass * Mathf.Sqrt(frequency * 2 * Mathf.Pi);
  }

  private static float CalculateDamping(float stiffness, float mass, float zeta)
  {
    return zeta * 2 * mass * Mathf.Sqrt(stiffness / mass);
  }

  private void RecalculateSprings(VehicleBody3D vehicle)
  {
    if (!vehicle.TryGetMeta(PROPERTY_FREQUENCY, out float frequency))
    {
      frequency = 1;
    }
    if (!vehicle.TryGetMeta(PROPERTY_ZETA, out float zeta))
    {
      zeta = 1;
    }


    var springs = vehicle.GetChildren<VehicleWheel3D>();
    var relMass = vehicle.Mass / springs.Count;

    var stiffness = CalculateStiffness(relMass, frequency);
    var damping = CalculateDamping(stiffness, relMass, zeta);

    foreach (var spring in springs)
    {
      spring.SuspensionStiffness = stiffness;
      spring.DampingCompression = damping;
      spring.DampingRelaxation = damping;
    }
  }

}
