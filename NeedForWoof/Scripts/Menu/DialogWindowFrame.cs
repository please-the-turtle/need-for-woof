using Godot;
using System;

public class DialogWindowFrame : NinePatchRect
{
    private AnimationPlayer _animationPlayer; 
    
    public override void _Ready()
    {
        SetChildrenVisible(false);
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animationPlayer.CurrentAnimation = "open";
    }

    public void SetChildrenVisible(bool visible)
    {
        var children = GetChildren();
        foreach (Node child in children)
        {
            if (child.GetType() == typeof(Control))
            {
                Control control = (Control) child;
                control.Visible = visible;
            }
        }
    }

}
