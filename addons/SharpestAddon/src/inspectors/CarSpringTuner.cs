using Godot;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using rosthouse.sharpest.addon;
using System;

public partial class CarSpringTuner : EditorInspectorPlugin
{
  private const string PROPERTY_ZETA = "Zeta";
  private const string PROPERTY_FREQUENCY = "Frequency";
  private const string PROPERTY_TRAVEL = "Travel";

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

      EditorSpinSlider travel = new EditorSpinSlider
      {
        MinValue = 0,
        Step = 0,
        Label = "Travel",
        Suffix = "m",
        HideSlider = true,

      };

      if (@object.TryGetMeta<float>(PROPERTY_FREQUENCY, out var frequencyValue))
      {
        frequency.SetValueNoSignal(frequencyValue);
      }

      if (@object.TryGetMeta<float>(PROPERTY_ZETA, out var zetaValue))
      {
        zeta.SetValueNoSignal(zetaValue);
      }

      if (@object.TryGetMeta<float>(PROPERTY_TRAVEL, out var travelValue))
      {
        travel.SetValueNoSignal(travelValue);
      }
      frequency.ValueChanged += v => OnFrequencyChanged(vehicle, v);
      zeta.ValueChanged += v => OnZetaChanged(vehicle, v);
      travel.ValueChanged += v => OnTravelChanged(vehicle, v);

      AddPropertyEditor(PROPERTY_FREQUENCY, frequency, label: "Frequency");
      AddPropertyEditor(PROPERTY_ZETA, zeta, label: "Zeta");
      AddPropertyEditor(PROPERTY_TRAVEL, travel, label: "Travel");
    }
  }

  private void OnTravelChanged(VehicleBody3D vehicle, double v)
  {
    vehicle.SetMeta(PROPERTY_TRAVEL, v);
    RecalculateSprings(vehicle);
  }

  private void OnZetaChanged(VehicleBody3D vehicle, double v)
  {
    vehicle.SetMeta(PROPERTY_ZETA, v);
    RecalculateSprings(vehicle);
  }

  private void OnFrequencyChanged(VehicleBody3D vehicle, double value)
  {
    vehicle.SetMeta(PROPERTY_FREQUENCY, value);
    RecalculateSprings(vehicle);
  }

  private static float CalculateStiffness(float mass, float frequency)
  {
    return mass * Mathf.Pow(frequency * 2 * Mathf.Pi, 2);
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
    if (!vehicle.TryGetMeta(PROPERTY_TRAVEL, out float travel))
    {
      travel = 0.2f;
    }

    var springs = vehicle.GetChildren<VehicleWheel3D>();
    // var relMass = vehicle.Mass;

    GD.Print($"Mass {vehicle.Mass}, frequency {frequency}");
    var stiffness = CalculateStiffness(vehicle.Mass, frequency);
    var damping = CalculateDamping(stiffness, vehicle.Mass, zeta);
    GD.Print($"Stiffness {stiffness}, damping {damping}");

    foreach (var spring in springs)
    {
      spring.SuspensionTravel = travel;
      var travelFactor = spring.SuspensionTravel * 1000; // convert travel to mm
      spring.SuspensionStiffness = stiffness / travelFactor;
      spring.DampingCompression = damping / travelFactor;
      spring.DampingRelaxation = damping / travelFactor;
    }
  }

}
