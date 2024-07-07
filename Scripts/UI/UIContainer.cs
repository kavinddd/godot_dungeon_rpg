using Godot;
using System;

public partial class UIContainer : Container
{
	[Export] public ContainerType Container { get; private set; }
	[Export] public Button Button { get; private set; }
	[Export] public TextureRect TextureRect { get; private set; }
	[Export] public Label Label { get; private set; }


}
