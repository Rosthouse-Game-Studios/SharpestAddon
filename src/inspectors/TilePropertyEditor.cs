#if TOOLS
using Godot;

namespace rosthouse.sharpest.addon.inspectors;

[Tool]
public partial class TilePropertyEditor : EditorProperty
{
    // The main control for editing the property.
    private EditorSpinSlider _propertyControl = new EditorSpinSlider();
    private double TileSize =>
        ProjectSettings.GetSetting(Constants.Settings.TileSizeSetting, 16).AsDouble();

    public TilePropertyEditor()
    {
        // Add the control as a direct child of EditorProperty node.
        _propertyControl.MaxValue = double.MaxValue;
        _propertyControl.MinValue = 0;
        _propertyControl.ControlState = EditorSpinSlider.ControlStateEnum.Hide;
        _propertyControl.Step = 0.01;

        AddChild(_propertyControl);
        // Make sure the control is able to retain the focus.
        AddFocusable(_propertyControl);
        // Setup the initial state and connect to the signal to track changes.
        _propertyControl.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        var calculatedValue = value * TileSize;
        GD.Print($"Setting value {calculatedValue} for input {value}");
        GetEditedObject().Set(GetEditedProperty(), calculatedValue);
    }

    public override void _UpdateProperty()
    {
        // Read the current value from the property.
        var propertyValue = GetEditedObject().Get(GetEditedProperty()).AsDouble();
        var newValue = propertyValue / TileSize;
        GD.Print($"Using value {newValue} for input {propertyValue}");
        if (newValue == _propertyControl.Value)
        {
            return;
        }
        _propertyControl.SetValueNoSignal(newValue);
    }
}

#endif
