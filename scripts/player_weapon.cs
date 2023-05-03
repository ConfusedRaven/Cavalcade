namespace Cavalcade;

public partial class player_weapon : Area2D
{
	[Export] private string _activeWeapon;
	[Export] private string _activeWeaponAnimation;
	[Export] public static int Damage = 1;
 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Sprite2D>(_activeWeapon).Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		if (Input.IsActionPressed("click"))
		{
			LookAt(GetGlobalMousePosition());
			GetNode<Sprite2D>(_activeWeapon).Visible = true;
			anim.Play(_activeWeaponAnimation);
			await ToSignal(anim, "animation_finished");
			GetNode<Sprite2D>(_activeWeapon).Visible = false;
		}
		else
		{
			await ToSignal(anim, "animation_finished");
			GetNode<Sprite2D>(_activeWeapon).Visible = false;
		}
	}
}
