using System;
using Godot;
using NeedForWoof.Scripts.Globals;

namespace NeedForWoof.Scripts.SettingsScreen
{
	public class PortLine : LineEdit
	{
		private Global _global;

		public override void _Ready()
		{
			base._Ready();

			_global = GetNode<Global>("/root/Global");
			Text = _global.GameSettings.NetworkPort.ToString();
		}

		public void OnPortLine_text_changed(string newText)
		{
			int newPort;
			if (Int32.TryParse(newText, out newPort))
			{
				_global.GameSettings.NetworkPort = newPort;
			}
		}
	}
}
