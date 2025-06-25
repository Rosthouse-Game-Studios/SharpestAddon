using Godot;
using rosthouse.sharpest.addon;
using System;

public partial class CarSpringTuner : EditorInspectorPlugin
{

  HSlider stiffnessSlider = new HSlider();
  HSlider dampingSlider = new HSlider();

  public override bool _CanHandle(GodotObject @object)
  {
    return @object is VehicleBody3D;
  }

  public override void _ParseCategory(GodotObject @object, string category)
  {
    base._ParseCategory(@object, category);
    if (category == nameof(VehicleBody3D))
    {
      var tunerProperty = new CarSpringStiffnessTunerProperty();
      tunerProperty.PropertyChanged += (p, v, f, c) => GD.Print($"Changed to {v}");
      AddPropertyEditor("Suspension/Stiffness", tunerProperty);
    }
  }

  private void RecalculateSprings(VehicleBody3D vehicle)
  {
    if (!vehicle.TryGetMeta("Stiffness", out float stiffness))
    {
      stiffness = 1;
    }
    if (!vehicle.TryGetMeta("Damping", out float damping))
    {
      damping = 1;
    }

    var springs = vehicle.GetChildren<VehicleWheel3D>();
    var relMass = vehicle.Mass / springs.Count;

    foreach (var spring in springs)
    {

    }

  }

}
