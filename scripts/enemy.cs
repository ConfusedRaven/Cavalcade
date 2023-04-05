using Godot;

public partial class enemy : CharacterBody2D
{
	[Signal] 
	public delegate void DeathEventHandler();
	
	[Export] private string _skinOne;
	[Export] private string _skinTwo;
	[Export] private int _speed = 16;
	[Export] public static int Damage = 1;
	[Export] private string _cavalcadePos;
	[Export] private string _playerPos;
	[Export] private int _health;
	private bool _playerDetected;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Show();
		GetNode<CollisionShape2D>("EnemyCollider").Disabled = false;
		GetNode<CollisionShape2D>("HurtBox/HurtBoxCollider").Disabled = false;
		GetNode<CollisionShape2D>("HitBox/HitBoxCollider").Disabled = false;
		_playerDetected = false;
		var rand = GD.RandRange(0, 1);
		if (rand == 0)
		{
			GetNode<Sprite2D>(_skinOne).Visible = true;
			GetNode<Sprite2D>(_skinTwo).Visible = false;
		}
		else if (rand == 1)
		{
			GetNode<Sprite2D>(_skinOne).Visible = false;
			GetNode<Sprite2D>(_skinTwo).Visible = true;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_health <= 0)
		{
			Hide();
			GetNode<CollisionShape2D>("EnemyCollider").Disabled = true;
			GetNode<CollisionShape2D>("HurtBox/HurtBoxCollider").Disabled = true;
			GetNode<CollisionShape2D>("HitBox/HitBoxCollider").Disabled = true;
			EmitSignal(SignalName.Death);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		switch (_playerDetected)
		{
			case true:
				Velocity = GlobalPosition.DirectionTo(GetNode<CharacterBody2D>(_playerPos).GlobalPosition) * _speed;
				LookAt(GetNode<CharacterBody2D>(_playerPos).GlobalPosition);
				anim.Play("walk");
				MoveAndSlide();
				break;
			case false:
				Velocity = GlobalPosition.DirectionTo(GetNode<StaticBody2D>(_cavalcadePos).Position) * _speed;
				LookAt(GetNode<StaticBody2D>(_cavalcadePos).GlobalPosition);
				anim.Play("walk");
				MoveAndSlide();
				break;
		}
	}

	private void _on_tracking_area_body_entered(CollisionObject2D player)
	{
		if (player == GetNode<CharacterBody2D>("../../Cavalcade/Player"))
		{
			_playerDetected = true;
		}
	}

	private void _on_tracking_area_body_exited(CollisionObject2D player)
	{
		if (player == GetNode<CharacterBody2D>("../../Cavalcade/Player"))
		{
			_playerDetected = false;
		}
	}

	private async void _on_hit_box_area_entered(CollisionObject2D hitBox)
	{
		if (hitBox != GetNode<Area2D>("../../Cavalcade/Player/PlayerWeapon")) return;
		var eff = GetNode<AnimationPlayer>("EffectPlayer");
		_health = _health - player_weapon.Damage;
		eff.Play("hit");
		await ToSignal(eff, "animation_finished");
	}
}
