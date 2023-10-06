using Godot;
using System;

namespace rosthouse.sharpest.addons
{
  public partial class WindowManager : Node
  {

    private static WindowManager _instance;
    public static WindowManager Instance => _instance;


    public override void _EnterTree()
    {
      if (_instance != null)
      {
        this.QueueFree();
      }
      else
      {
        _instance = this;
      }
    }

    public void OpenWindow(Control n)
    {
      this.OpenWindow(n, GetViewport().GetVisibleRect().Size / 2);
    }

    public void OpenWindow(Control windowContent, Vector2 position, string title = "")
    {
      var lw = GD.Load<PackedScene>("res://addons/SharpestAddon/Nodes/light_window.tscn").Instantiate<LightWindow>();
      lw.SetContent(windowContent, true);
      lw.SetTitle(title);
      lw.Position = position;
      this.AddChild(lw);
    }

    public void OpenWindow(Control windowContent, Vector3 position)
    {
      var screenPos = GetWindow().GetCamera3D().UnprojectPosition(position);
      this.OpenWindow(windowContent, screenPos);
    }

    public void OpenWindowTruncated(Control windowContent, Vector3 position)
    {
      var screenPos = GetWindow().GetCamera3D().UnprojectPosition(position);
      if (GetWindow().GetVisibleRect().HasPoint(screenPos))
      {
        this.OpenWindow(windowContent, screenPos);
      }
      else
      {
        this.OpenWindow(windowContent, GetViewport().GetVisibleRect().Size / 2);
      }
    }

    public void OpenPopup(Popup windowContent, Vector3 position)
    {
      var screenPos = GetWindow().GetCamera3D().UnprojectPosition(position);
      this.OpenPopup(windowContent, screenPos);
    }

    public void OpenPopup(Popup popup, Vector2 position)
    {

      popup.FocusExited += () => popup.QueueFree();
      this.AddChild(popup);
      popup.Popup(new Rect2I(
       position.RountToInt(),
        popup.Size)
      );

    }
  }
}
