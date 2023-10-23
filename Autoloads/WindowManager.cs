using Godot;
using System;

namespace rosthouse.sharpest.addon
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

    public void OpenWindow(Control windowContent, Vector2 position, string title = "", bool useNativeWindow = true)
    {

      Node w;
      if (useNativeWindow)
      {
        var nw = new Window();
        nw.AddChild(windowContent);
        nw.Position = position.RountToInt();
        nw.WrapControls = true;
        nw.ContentScaleAspect = Window.ContentScaleAspectEnum.Expand;
        nw.ContentScaleMode = Window.ContentScaleModeEnum.CanvasItems;
        w = nw;
        nw.CloseRequested += () => nw.QueueFree();
      }
      else
      {
        var lw = GD.Load<PackedScene>("res://addons/SharpestAddon/Nodes/light_window.tscn").Instantiate<LightWindow>();
        lw.SetContent(windowContent, true);
        lw.SetTitle(title);
        lw.Passthrough = true;
        lw.RespectContentMinSize = true;
        lw.Position = position;
        w = lw;
      }
      windowContent.TreeExiting += () => w.QueueFree();
      this.AddChild(w);
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
