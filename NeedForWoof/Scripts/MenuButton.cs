using Godot;

namespace NeedForWoof.Scripts
{
    public class MenuButton : TextureButton
    {
        public void OnMenuButton_pressed()
        {
            Global global = GetNode<Global>("/root/Global");
            global.GotoScene("res://Scenes/Menu/MainMenu.tscn");
        }
    
    }
}
