using Godot;
using System;

public partial class splash_screen : Control
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		anim.Play("fade_out");
		await ToSignal(anim, "animation_finished");
		
		GetTree().ChangeSceneToFile("res://scenes/ui/main_menu/main_menu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
