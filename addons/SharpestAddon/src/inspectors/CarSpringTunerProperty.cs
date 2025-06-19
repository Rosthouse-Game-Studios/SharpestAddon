
using System;
using Godot;
using Godot.Collections;

public partial class CarSpringTunerProperty : EditorProperty
{
  private HSlider stiffnessSlider;
  private HSlider dampingSlider;

  public CarSpringTunerProperty() : base()
  {
    stiffnessSlider = new HSlider()
    {
      MinValue = 0,
      MaxValue = 1f
    };
    dampingSlider = new HSlider
    {
      MinValue = 0,
      MaxValue = 1f
    };

    Label = "Spring";
    UseFolding = true;

    // // stiffnessSlider.ValueChanged += OnStiffnessChanged;
    // // dampingSlider.ValueChanged += OnDampingChanged;

    // AddChild(AddRow("Stiffness", stiffnessSlider));
    // AddChild(AddRow("Damping", dampingSlider));
  }

  public override Array<Dictionary> _GetPropertyList()
  {
    Dict dict =  [("Stiffness", stiffnessSlider), ("Damping", dampingSlider)];

  }


  private static Control AddRow(string label, HSlider stiffnessSlider)
  {
    var vBox = new VBoxContainer();
    var stiffnessBox = new HBoxContainer();
    stiffnessBox.AddChild(new Label { Text = label });
    stiffnessBox.AddChild(stiffnessSlider);
    return vBox;
  }

  private void OnDampingChanged(double value)
  {
    GD.Print($"Damping: {value}");
  }

  private void OnStiffnessChanged(double value)
  {
    GD.Print($"Stiffness {value}");
  }
}
