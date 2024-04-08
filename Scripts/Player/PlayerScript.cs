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
    public bool IsAlive { get; set; } = true;

    [Signal]
    public delegate void KillPlayerEventHandler(PlayerScript player);

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
        if (!IsAlive)
            return;

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
        if (timer_trash_slowdown < MAX_TRASH_SLOWDOWN)
            timer_trash_slowdown += delta;

        if (!IsAlive)
            return;

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
        if (_time != 0.0f)
            _time = 0.0f;
        // rotate fish sprite in the direction of the movement

        if (timer_trash_slowdown < MAX_TRASH_SLOWDOWN)
        {
            movement /= 2.0f;
        }

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
        var sin = Mathf.Cos(_time * 5) * 0.6f;
        var vec_Left = Vector2.Left;
        var move = new Vector2(vec_Left.x, sin) * 37.0f;

        MoveAndSlide(move);
    }

    Node _parent;
    internal void Kill(fishing_rod rod)
    {
        IsAlive = false;
        _parent = GetParent().GetParent();
        Debug.WriteLine(_parent+"\t\t\t"+GetParent());

        _parent.RemoveChild(GetParent());
        rod.CallDeferred("add_child", GetParent());
        Position = new Vector2(24, -1128);
        Debug.WriteLine(Position);

        EmitSignal("KillPlayerEventHandler", this);
    }

    public void KillAndDisappear(fishing_rod rod)
    {
        Debug.WriteLine(GetParent() + "\t\t KILL\t\t"+ _parent);

        rod.RemoveChild(GetParent());
        _parent.CallDeferred("add_child", this);
        Position = new Vector2(-9999, -9999);
    }

    const float MAX_TRASH_SLOWDOWN = 5.0f;
    float timer_trash_slowdown = 10.0f;
    internal void ActivateTrashSlowDown()
    {
        timer_trash_slowdown = 0.0f;
    }
}
