using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

namespace rosthouse.sharpest.addon.test.extensions;

[TestSuite]
public class ColorExtensionsTest
{

  [TestCase(0, 180)]
  [TestCase(120, 300)]
  [TestCase(240, 60)]
  public void GivenColor_CalculatingComplementary_IsCorrect(float inputHue, float complementaryHue)
  {
    var input = ConvertHueInDegreesToColor(inputHue);
    var output = input.Complementary();

    var complementary = ConvertHueInDegreesToColor(complementaryHue);

    AssertThat(output.H).IsEqualApprox(complementary.H, 0.001f);
  }

  [TestCase]
  public void CalculatingComplementary_SaturationValueAlpha_RemainUntouched()
  {
    var input = ConvertHueInDegreesToColor(0, 0.5f, 0.5f, 0.5f);
    var output = input.Complementary();

    AssertThat(output.S).IsEqual(0.5f);
    AssertThat(output.V).IsEqual(0.5f);
    AssertThat(output.A).IsEqual(0.5f);
  }

  [TestCase(0, 150, 210)]
  [TestCase(120, 270, 330)]
  [TestCase(240, 30, 90)]
  public void GivenColor_CalculatingSplitComplementary_IsCorrect(float inputHue, float complementaryOne, float complementaryTwo)
  {
    var input = ConvertHueInDegreesToColor(inputHue);
    var (splitOne, splitTwo) = input.SplitComplementary();

    var expectedOne = ConvertHueInDegreesToColor(complementaryOne);
    var expectedTwo = ConvertHueInDegreesToColor(complementaryTwo);

    AssertThat(splitOne.H).IsEqualApprox(expectedOne.H, 0.01f).OverrideFailureMessage($"Expected {complementaryOne} but received {splitOne.H * 360}");
    AssertThat(splitTwo.H).IsEqualApprox(expectedTwo.H, 0.001f).OverrideFailureMessage($"Expected {complementaryOne} but received {splitTwo.H * 360}");
  }


  [TestCase]
  public void CalculatingSplitComplementary_SaturationValueAlpha_RemainUntouched()
  {
    var input = ConvertHueInDegreesToColor(0, 0.5f, 0.5f, 0.5f);
    var (outputA, outputB) = input.SplitComplementary();

    AssertThat(outputA.S).IsEqual(0.5f);
    AssertThat(outputA.V).IsEqual(0.5f);
    AssertThat(outputA.A).IsEqual(0.5f);

    AssertThat(outputB.S).IsEqual(0.5f);
    AssertThat(outputB.V).IsEqual(0.5f);
    AssertThat(outputB.A).IsEqual(0.5f);
  }

  [TestCase(0, 120, 240)]
  [TestCase(120, 240, 0)]
  [TestCase(240, 0, 120)]
  public void GivenColor_CalculatingTriadic_IsCorrect(float inputHue, float complementaryOne, float complementaryTwo)
  {
    var input = ConvertHueInDegreesToColor(inputHue);
    var (splitOne, splitTwo) = input.Triadic();

    var expectedOne = ConvertHueInDegreesToColor(complementaryOne);
    var expectedTwo = ConvertHueInDegreesToColor(complementaryTwo);

    AssertThat(splitOne.H).IsEqualApprox(expectedOne.H, 0.001f);
    AssertThat(splitTwo.H).IsEqualApprox(expectedTwo.H, 0.001f);
  }

  [TestCase]
  public void CalculatingTriadic_SaturationValueAlpha_RemainUntouched()
  {
    var input = ConvertHueInDegreesToColor(0, 0.5f, 0.5f, 0.5f);
    var (outputA, outputB) = input.SplitComplementary();

    AssertThat(outputA.S).IsEqual(0.5f);
    AssertThat(outputA.V).IsEqual(0.5f);
    AssertThat(outputA.A).IsEqual(0.5f);

    AssertThat(outputB.S).IsEqual(0.5f);
    AssertThat(outputB.V).IsEqual(0.5f);
    AssertThat(outputB.A).IsEqual(0.5f);
  }


  [TestCase(0, 90, 180, 270)]
  [TestCase(120, 210, 300, 30)]
  [TestCase(240, 330, 60, 150)]
  public void GivenColor_CalculatingTetradic_IsCorrect(float inputHue, float complementaryOne, float complementaryTwo, float complementaryThree)
  {
    var input = ConvertHueInDegreesToColor(inputHue);
    var (splitOne, splitTwo, splitThree) = input.Tetradic();

    var expectedOne = ConvertHueInDegreesToColor(complementaryOne);
    var expectedTwo = ConvertHueInDegreesToColor(complementaryTwo);
    var expectedThree = ConvertHueInDegreesToColor(complementaryThree);

    AssertThat(splitOne.H).IsEqualApprox(expectedOne.H, 0.001f);
    AssertThat(splitTwo.H).IsEqualApprox(expectedTwo.H, 0.001f);
    AssertThat(splitThree.H).IsEqualApprox(expectedThree.H, 0.001f);
  }

  [TestCase]
  public void CalculatingTetradic_SaturationValueAlpha_RemainUntouched()
  {
    var input = ConvertHueInDegreesToColor(0, 0.5f, 0.5f, 0.5f);
    var (outputA, outputB, outputC) = input.Tetradic();

    AssertThat(outputA.S).IsEqual(0.5f);
    AssertThat(outputA.V).IsEqual(0.5f);
    AssertThat(outputA.A).IsEqual(0.5f);

    AssertThat(outputB.S).IsEqual(0.5f);
    AssertThat(outputB.V).IsEqual(0.5f);
    AssertThat(outputB.A).IsEqual(0.5f);

    AssertThat(outputC.S).IsEqual(0.5f);
    AssertThat(outputC.V).IsEqual(0.5f);
    AssertThat(outputC.A).IsEqual(0.5f);
  }

  [TestCase(0, 30, 60, 90)]
  [TestCase(120, 150, 180, 210)]
  [TestCase(240, 270, 300, 330)]
  public void GivenColor_CalculatingAnalogous_IsCorrect(float inputHue, float complementaryOne, float complementaryTwo, float complementaryThree)
  {
    var input = ConvertHueInDegreesToColor(inputHue);
    var (splitOne, splitTwo, splitThree) = input.Analogous();

    var expectedOne = ConvertHueInDegreesToColor(complementaryOne);
    var expectedTwo = ConvertHueInDegreesToColor(complementaryTwo);
    var expectedThree = ConvertHueInDegreesToColor(complementaryThree);

    AssertThat(splitOne.H).IsEqualApprox(expectedOne.H, 0.001f);
    AssertThat(splitTwo.H).IsEqualApprox(expectedTwo.H, 0.001f);
    AssertThat(splitThree.H).IsEqualApprox(expectedThree.H, 0.001f);
  }

  [TestCase]
  public void CalculatingAnalogous_SaturationValueAlpha_RemainUntouched()
  {
    var input = ConvertHueInDegreesToColor(0, 0.5f, 0.5f, 0.5f);
    var (outputA, outputB, outputC) = input.Tetradic();

    AssertThat(outputA.S).IsEqual(0.5f);
    AssertThat(outputA.V).IsEqual(0.5f);
    AssertThat(outputA.A).IsEqual(0.5f);

    AssertThat(outputB.S).IsEqual(0.5f);
    AssertThat(outputB.V).IsEqual(0.5f);
    AssertThat(outputB.A).IsEqual(0.5f);

    AssertThat(outputC.S).IsEqual(0.5f);
    AssertThat(outputC.V).IsEqual(0.5f);
    AssertThat(outputC.A).IsEqual(0.5f);
  }


  private static Color ConvertHueInDegreesToColor(float hue, float saturation = 1, float value = 1, float alpha = 1)
  {
    return Color.FromHsv(hue / 360.0f, saturation, value, alpha);
  }
}
