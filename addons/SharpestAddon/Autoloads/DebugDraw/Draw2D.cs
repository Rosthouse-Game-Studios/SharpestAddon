using System;
using System.Collections.Generic;
using Godot;

namespace rosthouse.sharpest.addon.autoloads.debug;



public partial class Draw2D : Control
{

  private enum ItemType
  {
    Point, Line,
    Circle, Rectangle
  }

  private struct Item
  {
    public Vector2[] points;
    public Color color;
    public ItemType type;
    public float width;
  }

  public static Draw2D Instance => _instance;
  private static readonly Color defaultColor;
  private static Draw2D _instance = null!;
  private Queue<Item> items = new();

  static Draw2D()
  {
    defaultColor = Colors.WhiteSmoke;
  }

  public override void _EnterTree()
  {
    base._EnterTree();
    if (_instance != null)
    {
      QueueFree();
      return;
    }
    _instance = this;
    items = new Queue<Item>();
  }

  public override void _Draw()
  {
    base._Draw();

    while (items.Count > 0)
    {
      var item = items.Dequeue();
      switch (item.type)
      {
        case ItemType.Point:
          DrawCircle(item.points[0], 1, item.color);
          break;
        case ItemType.Line:
          DrawLine(item.points[0], item.points[1], item.color, item.width, true);
          break;
        case ItemType.Circle:
          DrawCircle(item.points[0], item.width, item.color);
          break;
        case ItemType.Rectangle:
          DrawPolyline(item.points, item.color, item.width, true);
          break;
      }

    }
  }

  public void Point(Vector2 position)
  {
    Point(position, defaultColor);
  }

  public void Point(Vector2 position, Color c)
  {
    items.Enqueue(new Item()
    {
      points = new Vector2[] { position },
      color = c,
      type = ItemType.Point,
      width = 1
    });
  }

  public void Line(Vector2 start, Vector2 end, float width = 1)
  {
    Line(start, end, defaultColor, width);
  }

  public void Line(Vector2 start, Vector2 end, Color c, float width = 1)
  {
    items.Enqueue(new Item()
    {
      points = new Vector2[] { start, end },
      color = c,
      type = ItemType.Line,
      width = width
    });
  }

  public void Vector(Vector2 position, Vector2 direction, float width = 1)
  {
    Vector(position, direction, defaultColor, width);
  }

  public void Vector(Vector2 position, Vector2 direction, Color c, float width = 1)
  {
    var s = GetViewport().GetVisibleRect().Size;
    items.Enqueue(new Item()
    {
      points = new Vector2[] { position * s, (position + direction) * s },
      color = c,
      type = ItemType.Line,
      width = width
    });
  }
}
