
using System.Linq;
using GdUnit4;
using Godot;
using rosthouse.sharpest.addon.utils;
using static GdUnit4.Assertions;

namespace rosthouse.sharpest.addon.test.utils;

[TestSuite]
public class TextGeneratorTest {


  [BeforeTest]
  public void Before(){
    GD.Seed(0);
    TextGenerator.Reset();
  }

  [TestCase]
  public void FirstWord_Is_Lorem() {
    AssertThat(TextGenerator.GetWord()).IsEqual("lorem");
  }

  [TestCase]
  public void GetSentence_WithoutPunctuation_ContainsNoPunctuation(){
    AssertThat(TextGenerator.GetSentence(20, false)).NotContains(".");
    AssertThat(TextGenerator.GetSentence(20, false)).NotContains(",");
  }


  [TestCase]
  public void GetSentence_WithTwoGenerated_AreNotEqual(){
    var first = TextGenerator.GetSentence(20, true);
    var second = TextGenerator.GetSentence(20, true);
    AssertThat(first).IsNotEqual(second);
  }

  [TestCase]
  public void GetSentence_WithPunctuation_ContainsNoPunctuation(){
    AssertThat(TextGenerator.GetSentence(20, true)).Contains(",");
    AssertThat(TextGenerator.GetSentence(20, true).Last().ToString()).IsEqual(".");
  }
}
