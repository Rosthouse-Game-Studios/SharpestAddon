using GdUnit4;
using static GdUnit4.Assertions;

namespace rosthouse.sharpest.addon.test;

[TestSuite]
public class PluginSetup
{
  [TestCase]
  public void AllAutoloadsLoaded()
  {
    var sceneRunner = ISceneRunner.Load("res://test/TestScene.tscn");
    var node = sceneRunner.FindChild("TestScene");
    AssertObject(node).IsNotNull();
  }
}