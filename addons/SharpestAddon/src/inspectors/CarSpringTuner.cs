using Godot;
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
      var tunerProperty = new CarSpringTunerProperty();
      tunerProperty.PropertyChanged += (p, v, f, c) => GD.Print("Changed");
      AddCustomControl(new CarSpringTunerProperty());
      // AddPropertyEditor("Stiffness", new CarSpringTunerProperty(), false, "Stiffness");
    }
  }
}
