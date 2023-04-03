using System.Threading;
using Godot;
using Timer = Godot.Timer;

public partial class cavalcade : StaticBody2D
{
	[Export] public static int Health = 21;
	private int _doDamage = 0;
	
	public override void _Process(double delta)
	{
		if (Health <= 0)
		{
			GetTree().ChangeSceneToFile("res://scenes/ui/end_screen.tscn");
		}
	}

	private void _on_hit_box_area_entered(CollisionObject2D hitBox)
	{
		string[] spawners = {"../SpawnerLeft/MouseEnemy/HurtBox", "../SpawnerRight/MouseEnemy/HurtBox", "../SpawnerBottom/MouseEnemy/HurtBox"};
		foreach (var hurtBox in spawners)
		{
			if (hitBox != GetNode<Area2D>(hurtBox)) return;
			_doDamage = 1;
		}
	}
	private void _on_hit_box_area_exited(CollisionObject2D hitBox)
	{
		string[] spawners = {"../SpawnerLeft/MouseEnemy/HurtBox", "../SpawnerRight/MouseEnemy/HurtBox", "../SpawnerBottom/MouseEnemy/HurtBox"};
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
