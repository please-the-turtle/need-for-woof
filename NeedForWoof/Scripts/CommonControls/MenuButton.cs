using Godot;
using NeedForWoof.Scripts.Globals;

namespace NeedForWoof.Scripts.CommonControls
{
    public class MenuButton : Button
    {
        public void OnMenuButton_pressed()
        {
            if (GetTree().HasNetworkPeer())
            {
                // var network = GetNode<Network>("/root/Network");
                // if (GetTree().IsNetworkServer())
                // {
                //     network.RemoveServer();
                // }
                // else
                // {
                //     network.LeaveServer();
                // }
            }
            
            
            Global global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/MainMenu.tscn");
        }
    
    }
}
