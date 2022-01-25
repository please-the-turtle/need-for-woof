using System;
using Godot;

namespace NeedForWoof.SettingsScreen
{
	public class PortSpinBox : SpinBox
	{
		private Global _global;

		public override void _Ready()
		{
			base._Ready();

			_global = GetNode<Global>("/root/Global");
			Value = _global.GameSettings.NetworkPort;
		}

		public void OnPortSpinBox_value_changed(float value)
		{
			_global.GameSettings.NetworkPort = (int)value;
		}
	}
}
