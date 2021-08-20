using Godot;
using System;
using NeedForWoof.Scripts;

public class OnScreenInfo : CanvasLayer
{
    [Export] public NodePath Dog;

    private Dog _dog;

    private TextureProgress _staminaBar;

    public override void _Ready()
    {
        _dog = GetNode<Dog>(Dog);

        _staminaBar = GetNode<TextureProgress>("StaminaBar");
        _staminaBar.MaxValue = _dog.MaxStamina;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        _staminaBar.Value = _dog.Stamina;
    }
    
}
