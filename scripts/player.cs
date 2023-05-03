namespace Cavalcade;

public partial class player : CharacterBody2D
{
	[Export] public int Speed = 16;
	[Export] public static int Health = 11;
	private int _doDamage = 0;

	public override void _Process(double delta)
	{
		if (Health <= 0)
		{
			GetTree().ChangeSceneToFile("res://scenes/ui/end_screen.tscn");
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		// Get the input direction and handle the movement/deceleration.
		Velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (Velocity.Length() > 0)
		{
			Velocity = Velocity.Normalized() * Speed;
			anim.Play("walk");
		}
		else
		{
			anim.Play("RESET");
		}
		MoveAndCollide(Velocity);
	}

	private void _on_hit_box_area_entered(CollisionObject2D hitBox)
	{
		string[] spawners = {"../../SpawnerLeft/MouseEnemy/HurtBox", "../../SpawnerRight/MouseEnemy/HurtBox", "../../SpawnerBottom/MouseEnemy/HurtBox"};
		foreach (var hurtBox in spawners)
		{
			if (hitBox != GetNode<Area2D>(hurtBox)) return;
			_doDamage = 1;
		}
	}
	
	private void _on_hit_box_area_exited(CollisionObject2D hitBox)
	{
		string[] spawners = {"../../SpawnerLeft/MouseEnemy/HurtBox", "../../SpawnerRight/MouseEnemy/HurtBox", "../../SpawnerBottom/MouseEnemy/HurtBox"};
		foreach (var hurtBox in spawners)
		{
			if (hitBox != GetNode<Area2D>(hurtBox)) return;
			_doDamage = 0;
		}
	}
	
	private async void _on_dps_timeout()
	{
		if (_doDamage != 1) return;
		var eff = GetNode<AnimationPlayer>("EffectPlayer");
		Health = Health - enemy.Damage;
		eff.Play("hit");
		await ToSignal(eff, "animation_finished");
	}
}
