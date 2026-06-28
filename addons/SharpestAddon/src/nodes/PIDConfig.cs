
using Godot;

namespace rosthouse.sharpest.addon.nodes;

[Tool]
[GlobalClass]
public partial class PIDConfig : Resource
{
  [Export] public float Proportional { get; set; }
  [Export] public float Integral { get; set; }
  [Export] public float Derivative { get; set; }
}

