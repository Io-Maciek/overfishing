using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScript : KinematicBody2D
{
	protected Node2D playerSprite;
	public AFish fish;
	public string device;

	bool horizontalVectorLeft = true;

	Vector2 _sprite_scale;
	public Node2D scene_root { get; set; }
	public PlayersInit MovementServer;
	public bool IsAlive { get; set; }

	public override void _Ready()
	{
	}


	


	// this script acts as '_Ready'
	public virtual void SetPlayer(PlayerUIModel player, PlayersInit movementServer)
	{
		fish = player.Fish;
		device = player.AssignedDevice.ToLower();
		//var texture = ResourceLoader.Load(fish.SpriteFullPath()) as Texture;
		playerSprite = (Node2D)GetNode("Sprite");
		//playerSprite.Texture = texture; // method _Ready does not run before this for some reason
		MovementServer = movementServer;
		_sprite_scale = playerSprite.Scale;
	}

	public override void _Process(float delta)
	{
		if (fish == null)
			return;

		if (scene_root == null)
			return;

		if (Input.IsActionJustPressed($"{device}_select"))
		{
			//Debug.WriteLine($"{fish}\t\t{GetTree().Root.GetNode("Scene").Name}");
			fish.Ability(scene_root);
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		// return if fish was not set yet
		if (fish == null)
			return;

		// get movement vetor
		if (MovementServer == null)
			return;
		Vector2 movement = MovementServer.MovementServer(device);


		// return if player did not moved
		if (movement == Vector2.Zero)
			_handleWaterGravity(delta);
		else
			_handleMovement(movement);
	}

    private void _handleMovement(Vector2 movement)
    {
		if(_time!=0.0f)
			_time = 0.0f;
        // rotate fish sprite in the direction of the movement
        var movingLeft = (movement.x < 0);

        if (horizontalVectorLeft != movingLeft)
        {
            horizontalVectorLeft = movingLeft;
            playerSprite.Scale = horizontalVectorLeft ? _sprite_scale : new Vector2(_sprite_scale.x * -1.0f, _sprite_scale.y);
        }

        horizontalVectorLeft = movingLeft;
        LookAt(GlobalPosition + (horizontalVectorLeft ? (-1 * movement) : movement));


        // apply movement
        //MoveAndCollide
        MoveAndSlide(movement);
    }


	float _time = 0.0f;
    private void _handleWaterGravity(float delta)
    {
		_time += delta;
		var sin = Mathf.Cos(_time*5)*0.6f;
		var vec_Left = Vector2.Left ;
		var move = new Vector2(vec_Left.x, sin) * 37.0f;

        MoveAndSlide(move);
    }
}
