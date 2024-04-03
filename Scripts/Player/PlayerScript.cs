using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScript : KinematicBody2D
{
    Sprite playerSprite;
    AFish fish;
    string device;

    public float MovementSpeed = 200.0f;
    bool horizontalVectorLeft = true;

    Vector2 _sprite_scale;

    public override void _Ready()
    {
    }


    public override void _Process(float delta)
    {

    }


    // this script acts as '_Ready'
    public void SetPlayer(PlayerUIModel player)
    {
        fish = player.Fish;
        device = player.AssignedDevice.ToLower();
        var texture = ResourceLoader.Load(fish.SpriteFullPath()) as Texture;
        playerSprite = (Sprite)GetNode("Sprite");
        playerSprite.Texture = texture; // method _Ready does not run before this for some reason

        _sprite_scale = playerSprite.Scale;
    }


    public override void _PhysicsProcess(float delta)
    {
        // return if fish was not set yet
        if (fish == null)
            return;

        // get movement vetor
        Vector2 movement = Input.GetVector($"{device}_left", $"{device}_right", $"{device}_up", $"{device}_down")
            * MovementSpeed;


        // return if player did not moved
        if (movement == Vector2.Zero)
            return;


        // rotate fish sprite in the direction of the movement
        var movingLeft = (movement.x < 0);

        if (horizontalVectorLeft != movingLeft)
        {
            horizontalVectorLeft = movingLeft;
            playerSprite.Scale = horizontalVectorLeft ? _sprite_scale : new Vector2(_sprite_scale.x * -1.0f, _sprite_scale.y);
        }

        horizontalVectorLeft = movingLeft;
        LookAt(GlobalPosition + (horizontalVectorLeft ?  (-1 * movement): movement));


        // apply movement
        //MoveAndCollide
        MoveAndSlide(movement);
    }
}
