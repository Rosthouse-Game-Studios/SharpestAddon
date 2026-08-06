#if TOOLS
using Godot;

namespace rosthouse.sharpest.addon.inspectors;

[Tool]
public partial class DegToRadEditorProperty : EditorProperty
{
  // The main control for editing the property.
  private EditorSpinSlider _propertyControl = new EditorSpinSlider();

  public DegToRadEditorProperty()
  {
    // Add the control as a direct child of EditorProperty node.
    _propertyControl.Suffix = "º";
    _propertyControl.MaxValue = double.MaxValue;
    _propertyControl.MinValue = 0;
    _propertyControl.ControlState = EditorSpinSlider.ControlStateEnum.Hide;

    AddChild(_propertyControl);
    // Make sure the control is able to retain the focus.
    AddFocusable(_propertyControl);
    // Setup the initial state and connect to the signal to track changes.
    _propertyControl.ValueChanged += OnValueChanged;
  }

  private void OnValueChanged(double value)
  {
    var mod = Mathf.PosMod(value, 360);
    GD.Print($"Value: {value}, Modded: {mod}");
    GetEditedObject().Set(GetEditedProperty(), Mathf.DegToRad(mod));
  }

  public override void _UpdateProperty()
  {
    // Read the current value from the property.
    var newValue = Mathf.RadToDeg(GetEditedObject().Get(GetEditedProperty()).AsDouble());
    if (newValue == _propertyControl.Value)
    {
      return;
    }
    _propertyControl.SetValueNoSignal(newValue);
  }
}
#endif
