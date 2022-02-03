using Godot;

namespace NeedForWoof
{
    public class MenuButton : Button
    {
        public void OnMenuButton_pressed()
        {
            Global global = GetNode<Global>("/root/Global");
            Network network = GetNode<Network>("/root/Network");

            if (GetTree().NetworkPeer != null)
            {
                network.Close();
            }
            
            global.GotoScene("res://Scenes/Menu/MainMenu.tscn");
        }
    
    }
}
