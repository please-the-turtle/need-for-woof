using Godot;

namespace NeedForWoof.Scripts.Level
{
    public class MenuTouchButton : TouchScreenButton
    {
        public void GotoMenu()
        {
            if (GetTree().HasNetworkPeer())
            {
                var network = GetNode<Network>("/root/Network");
                if (GetTree().IsNetworkServer())
                {
                    network.RemoveServer();
                }
                else
                {
                    network.LeaveServer();
                }
            }
            
            var global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/MainMenu.tscn");
        }
    }
}
