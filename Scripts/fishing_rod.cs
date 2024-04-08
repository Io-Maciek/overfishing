using Godot;
using System;
using System.Diagnostics;

public class fishing_rod : Node2D
{
	const string _path = "res://Images/Rod/";
	bool _image_chosen = false;
	const string _rod = "rodX.png";
	const string _bait = "baitX.png";
	static int[] x_range = new int[2] { 16, 1240 };
	static int[] y_range = new int[2] { 619, 1232};
	static int[] y_range_flipped = new int[2] { -545, 99};
	public static int y_range_start_down = 1427;
	public static int y_range_start_up = -690;
	bool is_flipped = false;
	bool kill_animation = false;


    Texture rod_texture;
	Texture bait_texture;

	Vector2 MoveToPosition;

	public float Speed = 100.0f;
	static int[] speed_range = new int[2] { 80, 400 };

	CanvasItemMaterial material;
	bool affected_by_light = false;

	public void ToggleAffectLight()
	{
		Debug.WriteLine("TOGGLE LIGHT TO ROD");
        affected_by_light = !affected_by_light;

        if (affected_by_light)
			material.LightMode = CanvasItemMaterial.LightModeEnum.LightOnly;
		else
			material.LightMode = CanvasItemMaterial.LightModeEnum.Normal;

    }

    public override void _Ready()
	{
		rod_texture = (GetNode("Rod1") as Sprite).Texture;
		bait_texture = (GetNode("Bait3") as Sprite).Texture;
		material = Material as CanvasItemMaterial;
		material.LightMode = CanvasItemMaterial.LightModeEnum.Normal;

		//Randomize(false, false); //TODO
	}

	int number = 0;

	public void Randomize(bool flipped_x, bool flipped_y, int rod_number)
	{
		if (_image_chosen)
			return;

		is_flipped = flipped_y;
		number = rod_number;

        _image_chosen = true;
		var r = new Random().Next(1, 4);
		var b = new Random().Next(1, 4);
		Debug.WriteLine($"{number}.\tWylosowano rod: {r} oraz bait {b}");


		var rod_resource = ResourceLoader.Load(_path + _rod.Replace('X', r.ToString()[0]));
		var bait_resource = ResourceLoader.Load(_path + _bait.Replace('X', b.ToString()[0]));

		var _scale = Scale;
		Scale = new Vector2(flipped_x ? -Scale.x : Scale.x, is_flipped ? -Scale.y : Scale.y);

		rod_texture = rod_resource as Texture;
		bait_texture = bait_resource as Texture;

		ChooseStart();
	}

	private void ChooseStart()
	{
		_time = 0.0f;
		Speed = new Random().Next(speed_range[0], speed_range[1]);

		// choose random X start position and default Y start start position
		var y_pos = is_flipped ? y_range_start_up : y_range_start_down;
		var x_pos = new Random().Next(x_range[0], x_range[1] + 1);
		Position = new Vector2(x_pos, y_pos);

		// set MoveToPosition vector as the same X as start position but randomize Y
		var y_random_pos = is_flipped ? new Random().Next(y_range_flipped[0], y_range_flipped[1] + 1) :
			new Random().Next(y_range[0], y_range[1] + 1);
		MoveToPosition = new Vector2(x_pos, y_random_pos);

		_direction = Position.DirectionTo(MoveToPosition);
		Debug.WriteLine($"{number}.\tPostion generated: " + MoveToPosition + "\tWith speed: " + Speed + "\tThe direction is: " + _direction);
	}

	Vector2 _direction;

	float _time = 0.0f;
	public override void _PhysicsProcess(float delta)
	{
		if (kill_animation)
			GoDirectlyTo(delta);
		else
		{
			var distance_to = (MoveToPosition - Position).LengthSquared();
			if (distance_to <= 7000.0f)
			{
				GoDirectlyTo(delta);
			}
			else
			{
				GoGentlyTo(delta);
			}
		}
	}

	private void GoDirectlyTo(float delta)
	{
		Position = Position.MoveToward(MoveToPosition, delta * Speed);

		if(Position == MoveToPosition)
		{
			if (kill_animation)
			{
				if(catch_fish != null)
				{
					catch_fish.KillAndDisappear();//.QueueFree();
					kill_animation = false;
					catch_fish = null;
                }
            }
            GoToNext();// TODO idk
		}
	}

	private void GoGentlyTo(float delta)
	{
		_time += delta;
		float offset = Mathf.Sin(_time * 5f) * 3; // Adjust the frequency and amplitude of the noise as needed

		// Calculate noisy movement vector
		var noisyMovement = new Vector2(-_direction.y, _direction.x) * offset;

		// Move along the direction with noisy movement
		Position += _direction * Speed * delta + noisyMovement;
		//Debug.WriteLine(Position > MoveToPosition);
	}

	private void GoToNext()
	{
		_time = 0.0f;
		Speed = new Random().Next(speed_range[0], speed_range[1]);

		var y_pos = is_flipped ? new Random().Next(y_range_flipped[0], y_range_flipped[1] + 1) :
			new Random().Next(y_range[0], y_range[1] + 1);
		MoveToPosition = new Vector2(new Random().Next(x_range[0], x_range[1] + 1), y_pos);
		//new Vector2(x_range[0], y_range[1]);//

		_direction = Position.DirectionTo(MoveToPosition);
		Debug.WriteLine($"{number}.\tPostion generated: " + MoveToPosition + "\tWith speed: "+ Speed+"\tThe direction is: "+ _direction);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	PlayerScript catch_fish = null;
    private void _on_Area2D_body_entered(object body)
	{
		if (kill_animation)
			return;

		if (!(body is PlayerScript))
			return;

		var player = (PlayerScript)body;
		var is_player = player != null;
		if (!is_player)
			return;

		if (!player.IsAlive)
			return;

		catch_fish = player;
        catch_fish.Kill(this);
		HandleStartKillAnimation();
	}

    private void HandleStartKillAnimation()
    {
		kill_animation = true;
        MoveToPosition = new Vector2(Position.x, is_flipped?y_range_start_up:y_range_start_down);
        _direction = Position.DirectionTo(MoveToPosition);
    }
}



