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

	Texture rod_texture;
	Texture bait_texture;

	Vector2 MoveToPosition;

	public float Speed = 100.0f;
	static int[] speed_range = new int[2] { 80, 400 };


    public override void _Ready()
	{
		rod_texture = (GetNode("Rod1") as Sprite).Texture;
		bait_texture = (GetNode("Bait3") as Sprite).Texture;


		Randomize(); //TODO
					 //TODO
	}

	public void Randomize()
	{
		if (_image_chosen)
			return;

		_image_chosen = true;
		var r = new Random().Next(1, 4);
		var b = new Random().Next(1, 4);
		Debug.WriteLine($"Wylosowano rod: {r} oraz bait {b}");


		var rod_resource = ResourceLoader.Load(_path + _rod.Replace('X', r.ToString()[0]));
		var bait_resource = ResourceLoader.Load(_path + _bait.Replace('X', b.ToString()[0]));

		rod_texture = rod_resource as Texture;
		bait_texture = bait_resource as Texture;

		GoToNext();
	}
	Vector2 _direction;

	float _time = 0.0f;
	public override void _PhysicsProcess(float delta)
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

    private void GoDirectlyTo(float delta)
    {
		Position = Position.MoveToward(MoveToPosition, delta * Speed);

		if(Position == MoveToPosition)
		{
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
        MoveToPosition = new Vector2(new Random().Next(x_range[0], x_range[1] + 1), new Random().Next(y_range[0], y_range[1] + 1));
        //new Vector2(x_range[0], y_range[1]);//

        _direction = Position.DirectionTo(MoveToPosition);
        Debug.WriteLine("Postion generated: " + MoveToPosition + "\tWith speed: "+ Speed+"\tThe direction is: "+ _direction);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    private void _on_Area2D_body_entered(object body)
	{
		if (!(body is PlayerScript))
			return;

		var player = (PlayerScript)body;
		var is_player = player != null;
		if (!is_player)
			return;

		player.Kill();
	}
}



