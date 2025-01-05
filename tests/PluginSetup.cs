using GdUnit4;
using rosthouse.sharpest.addon.autoloads;
using static GdUnit4.Assertions;

namespace rosthouse.sharpest.addon.tests;

[TestSuite]
public class PluginSetup
{
  [TestCase]
  public void AllAutoloadsLoaded()
  {
    AssertObject(Draw3D.Instance).IsNotNull();
    AssertObject(WindowManager.Instance).IsNotNull();
  }
}
