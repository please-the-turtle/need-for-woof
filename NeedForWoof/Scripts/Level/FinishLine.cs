using Godot;

namespace NeedForWoof.Level
{
    public class FinishLine : Area2D
    {
        public void OnFinishLine_body_exited(Node body)
        {
            if (body is Dog dog)
            {
                dog.Finish();
            }
        }
    }
}
