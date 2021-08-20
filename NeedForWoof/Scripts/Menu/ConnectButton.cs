using Godot;
using System;
using NeedForWoof.Scripts;

public class ConnectButton : TextureButton
{
    public void OnConnectButton_pressed()
    {
        var global = GetNode<Global>("/root/Global");
        global.GotoScene("res://Scenes/Menu/ConnectScreen.tscn");
    }
}
