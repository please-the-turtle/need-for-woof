using Godot;

namespace NeedForWoof.Level
{
    public class MenuTouchButton : TouchScreenButton
    {
        public void GotoMenu()
        {
            if (GetTree().HasNetworkPeer())
            {
                GetNode<Network>("/root/Network").Close();
            }
            
            var global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/MainMenu.tscn");
        }
    }
}
