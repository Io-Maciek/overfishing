using Godot;
using Overfishing.Player;
using Overfishing.Scripts.Fishes;
using System;
using System.Diagnostics;

public class PlayerScriptLantern : PlayerScript
{
    public Timer abilityTimer;
    public Light2D light;
    public ShaderMaterial shader;

    public override void SetPlayer(PlayerUIModel player, PlayersInit movementServer)
    {
        base.SetPlayer(player, movementServer);

		(playerSprite.GetNode("AnimationPlayer") as AnimationPlayer).Play("idle");
        abilityTimer = (Timer)GetNode("Timer");
        abilityTimer.Connect("timeout", this, "_lantern_ability_over");
        light = GetNode("Sprite").GetNode("Lantern").GetNode("Light2D") as Light2D;
        light.Enabled = false;

        shader = (ShaderMaterial)((TextureRect)movementServer.GetParent().GetNode("CanvasLayer2").GetNode("BackgroundWater")).Material;
        shader.SetShaderParam("ignore_light", true);
    }

    private void _lantern_ability_over()
    {
        foreach (var others in fish.GetOthers(null))
        {
            (((Sprite)others.GetNode("Sprite")).Material as CanvasItemMaterial).LightMode = CanvasItemMaterial.LightModeEnum.Normal;
        }

        shader.SetShaderParam("ignore_light", true);
        light.Enabled = false;
    }

    public void StartAbility()
    {
        shader.SetShaderParam("ignore_light", false);
        light.Enabled = true;
    }
}
