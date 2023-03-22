using Godot;
using System;

public partial class main_menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var anim2 = GetNode<AnimationPlayer>("AnimationPlayer2");
		anim2.Play("fade");
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		anim.Play("animate_ui");
	}
	private void _on_texture_button_button_up()
	{
		GetTree().ChangeSceneToFile("res://scenes/worlds/game.tscn");
	}
}
