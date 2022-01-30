using Godot;

namespace NeedForWoof
{
    public class MenuButton : Button
    {
        public void OnMenuButton_pressed()
        {
            Global global = GetNode<Global>("/root/Global");

            if (global.Network != null)
            {
                global.CloseNetworkConnection();
            }
            
            global.GotoScene("res://Scenes/Menu/MainMenu.tscn");
        }
    
    }
}
