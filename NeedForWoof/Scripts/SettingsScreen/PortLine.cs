using System;
using Godot;

namespace NeedForWoof.Scripts.SettingsScreen
{
	public class PortLine : LineEdit
	{
		public override void _Ready()
		{
			base._Ready();

			Text = Global.NetworkPort.ToString();
		}

		public void OnPortLine_text_changed(string newText)
		{
			int newPort;
			if (Int32.TryParse(newText, out newPort))
			{
				Global.NetworkPort = newPort;
			}
		}
	}
}
