using Godot;

namespace NeedForWoof.Level
{
    public class OnScreenInfo : CanvasLayer
    {
        private Dog _dog;

        private TextureProgress _staminaBar;

        public override void _Ready()
        {
            _dog = GetNode<DogsManager>("../YSort/Dogs").GetSelfDog();

            _staminaBar = GetNode<TextureProgress>("StaminaBar");
            _staminaBar.MaxValue = _dog.MaxStamina;
        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            UpdateStaminaBar();
        }

        private void UpdateStaminaBar()
        {
            _staminaBar.Value = _dog.Stamina;
        }
    
    }
}
