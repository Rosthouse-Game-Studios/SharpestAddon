using Godot;
using Godot.Collections;
using System.Linq;

namespace rosthouse.sharpest.addon;

[Tool]
[GlobalClass]
public partial class CsgStairs3D : CsgBox3D
{
  [Export] private int numStairs = 10;

  private int currentAmountOfStairs = -1;
  private Vector3 currentSize;
  private CsgPolygon3D stairsPolygon = null!;

  public override void _EnterTree()
  {
    var child = GetNodeOrNull<CsgPolygon3D>("StairsPolygon");
    if (child is null)
    {
      child = new CsgPolygon3D
      {
        Name = "StairsPolygon"
      };
      AddChild(child);
    }
    stairsPolygon = child;
  }

  public override void _Process(double delta)
  {
    if (currentAmountOfStairs != numStairs || currentSize != Size)
    {
      MakeStairs();
    }
  }

  private void MakeStairs()
  {
    if (!Engine.IsEditorHint())
    {
      return;
    }

    numStairs = Mathf.Clamp(numStairs, 0, 999);

    var stepHeight = Size.Y / numStairs;
    var stepWidth = Size.X / numStairs;

    // var stairs_poly = $StairsSubtractCSG#add_fresh_stairs_csg_poly();

    var pointArray = new Godot.Collections.Array<Vector2>();

    if (numStairs == 0)
    {
      MakeRamp(pointArray);
    }
    else
    {

      for (var i = 0; i < numStairs - 1; i++)
      {
        pointArray.Add(new Vector2(i * stepWidth, (i + 1) * stepHeight));
        if (i < numStairs)
        {
          pointArray.Add(new Vector2((i + 1) * stepWidth, (i + 1) * stepHeight));
        }
      }

      pointArray.Add(new Vector2(Size.X - stepWidth, Size.Y));
      pointArray.Add(new Vector2(0, Size.Y));

    }


    stairsPolygon.Polygon = pointArray.ToArray();
    stairsPolygon.Depth = Size.Z;
    stairsPolygon.Position = new Vector3
    {
      X = Size.Z / 2.0f,
      Y = -Size.Y / 2.0f,
      Z = -Size.X / 2.0f,
    };

    currentAmountOfStairs = numStairs;
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