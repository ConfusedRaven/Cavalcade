using Godot;

public partial class overworld : Node2D
{
	[Export] public PackedScene MonsterScene;
	
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		GetNode<Area2D>("Cavalcade/Player").Visible = false;
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		anim.Play("cavalcade_move");
		await ToSignal(anim, "animation_finished");
		GetNode<Area2D>("Cavalcade/Player").Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
