#nullable enable

using Godot;
using Godot.Collections;
using System.Linq;

namespace rosthouse.sharpest.addon;

[Tool]
[GlobalClass]
public partial class CsgStairs3D : CsgBox3D
{
  private int amountOfSteps = 10;

  [Export(PropertyHint.Range, "0, 999")]
  private int AmountOfSteps
  {
    get => amountOfSteps;
    set
    {
      amountOfSteps = value;
      MakeStairs();
    }
  }

  private bool relative = false;


  /// <summary>
  /// If set to true, the number of stairs will be set for a 1 meter cube. Meaning, if your stairs are 2 meters high and you set <see cref="CsgStairs3D.stepsToGenerate" /> to 10, you get 20 steps. 
  /// </summary>
  [Export]
  private bool Relative
  {
    get => relative;
    set
    {
      relative = value;
      MakeStairs();
    }
  }

  private Vector3 currentSize;
  private CsgPolygon3D? stairsPolygon;

  public override void _Ready()
  {
    stairsPolygon = GetNodeOrNull<CsgPolygon3D>("StairsPolygon");
    if (stairsPolygon is null)
    {
      stairsPolygon = new CsgPolygon3D
      {
        Name = "StairsPolygon",
        Operation = OperationEnum.Subtraction,
        Material = Material
      };
      AddChild(stairsPolygon);
      stairsPolygon.Owner = Owner;
    }
    MakeStairs();
  }

  public override void _Process(double delta)
  {
    if (!Engine.IsEditorHint())
    {
      return;
    }

    if (currentSize != Size)
    {
      MakeStairs();
    }

    if(stairsPolygon is not null && stairsPolygon.Material != Material){
      stairsPolygon.Material = Material;
    }
  }

  private void MakeStairs()
  {
    if (stairsPolygon == null)
    {
      return;
    }

    var stepsToGenerate = relative ? Mathf.RoundToInt(amountOfSteps * Size.Y) : amountOfSteps;

    var stepHeight = Size.Y / stepsToGenerate;
    var stepWidth = Size.X / stepsToGenerate;

    var pointArray = new Godot.Collections.Array<Vector2>();

    if (stepsToGenerate == 0)
    {
      MakeRamp(pointArray);
    }
    else
    {
      for (var i = 0; i < stepsToGenerate - 1; i++)
      {
        pointArray.Add(new Vector2(i * stepWidth, (i + 1) * stepHeight));
        if (i < stepsToGenerate)
        {
          pointArray.Add(new Vector2((i + 1) * stepWidth, (i + 1) * stepHeight));
        }
      }

      pointArray.Add(new Vector2(Size.X - stepWidth, Size.Y));
      pointArray.Add(new Vector2(0, Size.Y));
    }

    stairsPolygon.Polygon = pointArray.ToArray();
    stairsPolygon.Depth = Size.Z;
    stairsPolygon.Position = Vector3.Zero;
    stairsPolygon.Position = new Vector3
    {
      X = -Size.X / 2,
      Y = -Size.Y / 2,
      Z = Size.Z / 2,
    };

    currentSize = Size;
  }

  private void MakeRamp(Array<Vector2> pointArray)
  {
    pointArray.Add(new Vector2(Size.X, Size.Y));
    pointArray.Add(new Vector2(0, Size.Y));
    pointArray.Add(new Vector2(0, 0));
  }


  private string GetDebuggerDisplay()
  {
    return ToString();
  }
}