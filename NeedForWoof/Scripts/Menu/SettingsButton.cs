using Godot;
using System;
using NeedForWoof.Scripts;

public class SettingsButton : TextureButton
{
    public void OnSettingsButton_pressed()
    {
        var global = GetNode<Global>("/root/Global");
        global.GotoScene("res://Scenes/Menu/SettingsScreen.tscn");
    }
}
