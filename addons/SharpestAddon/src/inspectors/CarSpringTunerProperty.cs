
using Godot;
using rosthouse.sharpest.addon;

public partial class CarSpringStiffnessTunerProperty : EditorProperty
{
  private EditorSpinSlider stiffnessSlider;

  public CarSpringStiffnessTunerProperty() : base()
  {
    stiffnessSlider = new EditorSpinSlider()
    {
      MinValue = 0,
      MaxValue = 1f,
      Step = 0
    };


    Label = "Stiffness";
    stiffnessSlider.ValueChanged += OnStiffnessChanged;

    AddChild(stiffnessSlider);
  }

  public override void _ExitTree(){
    stiffnessSlider.ValueChanged -= OnStiffnessChanged;
  }
  public override void _UpdateProperty()
  {
    var editedObject = GetEditedObject();
    editedObject.SetMeta("Stiffness", stiffnessSlider.Value);
    EmitChanged("Stiffness", stiffnessSlider.Value);
  }

  private void OnStiffnessChanged(double value)
  {
    var editedObject = GetEditedObject();
    editedObject.SetMeta("Stiffness", stiffnessSlider.Value);
    EmitChanged("Stiffness", stiffnessSlider.Value);
  }
}
