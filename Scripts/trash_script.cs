using Godot;
using System;
using System.Diagnostics;
using System.Security.Policy;

public class trash_script : Node2D
{
	float base_scale;
	float new_scale;

	const int start_x = 1366;
	static int[] start_range_y = new int[] { 60, 665 };

	static string[] trash_name = new string[] { "res://Images/Trash/bag.png", "res://Images/Trash/bottle1.png", "res://Images/Trash/bottle2.png", "res://Images/Trash/bottle3.png", "res://Images/Trash/bottle4.png", "res://Images/Trash/glasses.png" };

	Sprite sprite;


	CanvasItemMaterial material;
	bool affected_by_light = false;

	public void ToggleAffectLightTrash()
	{
		Debug.WriteLine("TOGGLE LIGHT TO TRASH");
		affected_by_light = !affected_by_light;

		if (affected_by_light)
			(Material as CanvasItemMaterial).LightMode = CanvasItemMaterial.LightModeEnum.LightOnly;
		else
			(Material as CanvasItemMaterial).LightMode = CanvasItemMaterial.LightModeEnum.Normal;

	}


	GameObserver gameObserver;

	public void Start(GameObserver gameObserver, float base_scale = 0.07f)
	{
		this.gameObserver = gameObserver;
		sprite = (Sprite)GetNode("Sprite");

		var sprite_name = trash_name[new Random().Next(trash_name.Length)];
		sprite.Texture = ResourceLoader.Load<Texture>(sprite_name);

		RotationDegrees = new Random().Next(0, 360);
		this.base_scale = base_scale;
		new_scale = base_scale * new Random().Next(70, 130) / 100.0f;
		Scale = new Vector2(new_scale, new_scale);

		var max = 0.7f;
		var min = 0.3f;

		Modulate = new Color(Modulate.r, Modulate.g, Modulate.b, (float)new Random().NextDouble() * (max - min) + min);
		Position = new Vector2(start_x, new Random().Next(start_range_y[0], start_range_y[1]));

		speed = new Random().Next(75, 251);
	}

	float _time=.0f;
	int speed = 75;

	public override void _PhysicsProcess(float delta)
	{
		_time += delta;
		var sin = Mathf.Cos(_time * 5) * 0.6f;
		var move = new Vector2(-1.0f, sin);

		Position += move * delta * speed;

		if(Position.x <= -100)
		{
			//Debug.WriteLine("NISCZE SMIECI");
			gameObserver.DeleteMe(this);
		}
	}

	private void _on_Area2D_body_entered(object body)
	{
		if (!(body is PlayerScript))
			return;

		var player = (PlayerScript)body;
		var is_player = player != null;
		if (!is_player)
			return;

		if (!player.IsAlive)
			return;

		player.ActivateTrashSlowDown();
	}
}
