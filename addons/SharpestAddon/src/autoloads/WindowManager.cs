using Godot;

namespace rosthouse.sharpest.addon.autoloads;

public partial class WindowManager : Node
{

  private static WindowManager _instance = null!;
  public static WindowManager Instance => _instance;


  public override void _EnterTree()
  {
    if (_instance != null)
    {
      QueueFree();
    }
    else
    {
      _instance = this;
    }
  }

  public void OpenWindow(Control n)
  {
    OpenWindow(n, GetViewport().GetVisibleRect().Size / 2);
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
      var lw = GD.Load<PackedScene>("res://addons/resources/scenes/light_window.tscn").Instantiate<LightWindow>();
      lw.SetContent(windowContent, true);
      lw.SetTitle(title);
      lw.Passthrough = true;
      lw.RespectContentMinSize = true;
      lw.Position = position;
      w = lw;
    }
    windowContent.TreeExiting += () => w.QueueFree();
    AddChild(w);
  }

  public void OpenWindow(Control windowContent, Vector3 position)
  {
    var screenPos = GetWindow().GetCamera3D().UnprojectPosition(position);
    OpenWindow(windowContent, screenPos);
  }

  public void OpenWindowTruncated(Control windowContent, Vector3 position)
  {
    var screenPos = GetWindow().GetCamera3D().UnprojectPosition(position);
    if (GetWindow().GetVisibleRect().HasPoint(screenPos))
    {
      OpenWindow(windowContent, screenPos);
    }
    else
    {
      OpenWindow(windowContent, GetViewport().GetVisibleRect().Size / 2);
    }
  }

  public void OpenPopup(Popup windowContent, Vector3 position)
  {
    var screenPos = GetWindow().GetCamera3D().UnprojectPosition(position);
    OpenPopup(windowContent, screenPos);
  }

  public void OpenPopup(Popup popup, Vector2 position)
  {

    popup.FocusExited += () => popup.QueueFree();
    AddChild(popup);
    popup.Popup(new Rect2I(
     position.RountToInt(),
      popup.Size)
    );

  }
}
